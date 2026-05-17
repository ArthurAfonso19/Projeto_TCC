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

    [Header("Painel de Ações (Novos Botões)")]
    [SerializeField] private GameObject painelAcoes; // Opcional: Um grupo ou painel que engloba os botões abaixo
    [SerializeField] private Button botaoCarregar;
    [SerializeField] private Button botaoExcluir;

    private string saveSelecionado = "";

    private void Start()
    {
        ConfigurarBotoesAcao(false);
    }
    public void AbrirPainelLoad()
    {
        painelLoad.SetActive(true);
        saveSelecionado = "";
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
            novoBotao.GetComponent<Button>().onClick.AddListener(() => SelecionarSave(nomeTemporario));
        }
    }

    private void SelecionarSave(string nomeDoSave)
    {
        saveSelecionado = nomeDoSave;
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
            if(s != nomeParaRemover && !string.IsNullOrEmpty(s));
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
        if (painelAcoes != null) painelAcoes.SetActive(estado);
        if (botaoCarregar != null) botaoCarregar.interactable = estado;
        if (botaoExcluir != null) botaoExcluir.interactable = estado;
    }

}
