using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GerenciadorLoad : MonoBehaviour
{
    [Header("Configurações de UI")]
    [SerializeField] private GameObject painelLoad; 
    [SerializeField] private GameObject botaoSavePrefab; 
    [SerializeField] private Transform containerBotoes; 

    [Header("Botões de Ação (Independentes)")]
    [SerializeField] private GameObject objetoRaizCarregar; 
    [SerializeField] private GameObject objetoRaizExcluir;

    [Header("Customização Visual")]
    [SerializeField] private Color corNormal = Color.white; // Cor padrão do miolo do botão
    [SerializeField] private Color corSelecionado = new Color(0.7f, 0.85f, 1f); // Azul claro (exemplo)

    private string saveSelecionado = "";
    private Image imagemBotaoAnterior = null;

    private void Start()
    {
        ConfigurarBotoesAcao(false);
    }
    public void AbrirPainelLoad()
    {
        painelLoad.SetActive(true);
        saveSelecionado = "";
        imagemBotaoAnterior = null;
        ConfigurarBotoesAcao(false);

        foreach(Transform filho in containerBotoes)
        {
            Destroy(filho.gameObject);
        }

        string[] saves = DataManager.GetListadeSaves();
        foreach (string nomeSave in saves)
        {
            GameObject novoBotao = Instantiate(botaoSavePrefab, containerBotoes);
            novoBotao.GetComponentInChildren<TextMeshProUGUI>().text = nomeSave;

            string nomeTemporario = nomeSave;
            Button botaoComponente = novoBotao.GetComponent<Button>() ?? novoBotao.GetComponentInChildren<Button>();

            if(botaoComponente != null)
            {
                Image imgBotao = botaoComponente.targetGraphic as Image;
                botaoComponente.onClick.AddListener(() => SelecionarSave(nomeTemporario, imgBotao));
            }
        }
    }

    private void SelecionarSave(string nomeDoSave, Image imgBotao)
    {
        saveSelecionado = nomeDoSave;

        if(imagemBotaoAnterior != null)
        {
            imagemBotaoAnterior.color = corNormal;
        }

        if(imgBotao != null)
        {
            imgBotao.color = corSelecionado;
            imagemBotaoAnterior = imgBotao;    
        }

        ConfigurarBotoesAcao(true);
    }

    public void ClicouCarregar()
    {
        if(string.IsNullOrEmpty(saveSelecionado)) return;

        Time.timeScale = 1f;
        DataManager.CarregarDados(saveSelecionado);
        SceneManager.LoadScene("CenaArmazem");
    }

    public void ClicouExcluir()
    {
        if(string.IsNullOrEmpty(saveSelecionado)) return;

        PlayerPrefs.DeleteKey(saveSelecionado + "_Largura");
        PlayerPrefs.DeleteKey(saveSelecionado + "_Comprimento");
        PlayerPrefs.DeleteKey(saveSelecionado + "_Altura");

        AtualizarLista(saveSelecionado);
        AbrirPainelLoad();
    }

    private void AtualizarLista(string nomeParaRemover)
    {
        string[] savesAtuais = DataManager.GetListadeSaves();
        System.Collections.Generic.List<string> novaLista = new System.Collections.Generic.List<string>();

        foreach (string s in savesAtuais)
        {
            if(s != nomeParaRemover && !string.IsNullOrEmpty(s))
            {
                novaLista.Add(s);    
            }
        }

        string novaListaString = string.Join("|", novaLista);
        PlayerPrefs.SetString("ListaDeArmazens", novaListaString);
        PlayerPrefs.Save();
    }
    private void ConfigurarBotoesAcao (bool estado)
    {
        if (objetoRaizCarregar != null) objetoRaizCarregar.SetActive(estado);
        if (objetoRaizExcluir != null) objetoRaizExcluir.SetActive(estado);
    }

}
