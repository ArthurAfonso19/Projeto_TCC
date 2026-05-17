using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class DadosSetor
{
    public string nome;
    public int qtdRacks = 0;
    public float altura = 0f;
    public float largura = 0f;
    public float profundidade = 0f;
    public int qtdNiveis = 0;
    public bool configurado = false; // Controle para saber se já foi salvo ao menos uma vez
}
public class GerenciarSetores : MonoBehaviour
{
    [Header("Lista de Setores (Esquerda)")]
    [SerializeField] private Transform containerSetores; // O 'Content' do seu Scroll
    [SerializeField] private GameObject botaoSetorPrefab; // O Prefab do botão da lista
    [SerializeField] private GameObject botaoRemove; // O objeto inteiro do botão "REMOVE -"

    [Header("Pop-up Adicionar Setor")]
    [SerializeField] private GameObject popupNovoSetor; 
    [SerializeField] private TMP_InputField inputNomeSetor; 

    [Header("Painel Configurações da Prateleira (Direita)")]
    [SerializeField] private GameObject painelConfigPrateleira; // O quadro da direita inteiro
    [SerializeField] private TMP_InputField inputQtdRacks;
    [SerializeField] private TMP_InputField inputAlturaRack;
    [SerializeField] private TMP_InputField inputLarguraRack;
    [SerializeField] private TMP_InputField inputProfundidadeRack;
    [SerializeField] private TMP_InputField inputQtdNiveis;

    [Header("Customização Visual de Cores")]
    [SerializeField] private Color corNormal = Color.white; 
    [SerializeField] private Color corSelecionado = new Color(0.6f, 0.9f, 0.6f); // Verde para edição
    [SerializeField] private Color corConfigurado = new Color(0.6f, 0.75f, 0.9f); // Azul claro para salvo definitivo 

    private GameObject setorSelecionadoAtual = null; 
    private Image imagemBotaoAnterior = null;

    private Dictionary<GameObject, DadosSetor> bancoDeDadosSetores = new Dictionary<GameObject, DadosSetor>();

    private void Start()
    {
        FecharPopup();
        botaoRemove.SetActive(false);
        if(painelConfigPrateleira != null) painelConfigPrateleira.SetActive(false);
    }

    public void AbrirPopupNovoSetor()
    {
        popupNovoSetor.SetActive(true);
        inputNomeSetor.text = "";
    }

    public void FecharPopup()
    {
        popupNovoSetor.SetActive(false);
    }

    public void SalvarNovoSetor()
    {
        string nomeDoSetor = inputNomeSetor.text.Trim();

        if(string.IsNullOrEmpty(nomeDoSetor)) return;

        GameObject novoBotao = Instantiate(botaoSetorPrefab, containerSetores);
        novoBotao.GetComponentInChildren<TextMeshProUGUI>().text = nomeDoSetor;

        DadosSetor novosDados = new DadosSetor();
        novosDados.nome = nomeDoSetor;
        bancoDeDadosSetores.Add(novoBotao, novosDados);

        Button botaoComponente = novoBotao.GetComponent<Button>() ?? novoBotao.GetComponentInChildren<Button>();

        if(botaoComponente != null)
        {
            Image imgBotao = botaoComponente.targetGraphic as Image;
            botaoComponente.onClick.AddListener(() => SelecionarSetor(novoBotao, imgBotao));
            Debug.Log($"[SUCESSO] O botão para o setor '{nomeDoSetor}' foi gerado e o clique foi conectado!");
        }
        else
        {
            Debug.LogError($"[ERRO GRAVE] O Prefab '{botaoSetorPrefab.name}' NÃO tem um componente de 'Button' nele!");
        }

        FecharPopup();
    }

    private void SelecionarSetor(GameObject objetoBotaoClicado, Image imgBotao)
    {
        Debug.Log("[CLIQUE] A Unity detectou o clique no botão!");

        if(setorSelecionadoAtual == objetoBotaoClicado) return;

        if(imagemBotaoAnterior != null)
        {
            if(bancoDeDadosSetores.ContainsKey(setorSelecionadoAtual) && bancoDeDadosSetores[setorSelecionadoAtual].configurado)
            {
                imagemBotaoAnterior.color = corConfigurado;
            }
            else
            {
                imagemBotaoAnterior.color = corNormal;
            }
        }

        setorSelecionadoAtual = objetoBotaoClicado;
        imagemBotaoAnterior = imgBotao;

        if(imgBotao != null)
        {
            imgBotao.color = corSelecionado;
        }

        botaoRemove.SetActive(true);

        if(bancoDeDadosSetores.ContainsKey(objetoBotaoClicado))
        {
            DadosSetor dados = bancoDeDadosSetores[objetoBotaoClicado];
            
            inputQtdRacks.text = dados.qtdRacks == 0 ? "" : dados.qtdRacks.ToString();
            inputAlturaRack.text = dados.altura == 0f ? "" : dados.altura.ToString();
            inputLarguraRack.text = dados.largura == 0f ? "" : dados.largura.ToString();
            inputProfundidadeRack.text = dados.profundidade == 0f ? "" : dados.profundidade.ToString();
            inputQtdNiveis.text = dados.qtdNiveis == 0 ? "" : dados.qtdNiveis.ToString();
        }

        if(painelConfigPrateleira != null) painelConfigPrateleira.SetActive(true);
    }

    public void SalvarConfigPrateleira()
    {
        if(setorSelecionadoAtual == null || !bancoDeDadosSetores.ContainsKey(setorSelecionadoAtual)) return;


        DadosSetor dados = bancoDeDadosSetores[setorSelecionadoAtual];

        // 1. Coleta e converte o que o usuário digitou nos campos da direita
        int.TryParse(inputQtdRacks.text, out dados.qtdRacks);
        float.TryParse(inputAlturaRack.text, out dados.altura);
        float.TryParse(inputLarguraRack.text, out dados.largura);
        float.TryParse(inputProfundidadeRack.text, out dados.profundidade);
        int.TryParse(inputQtdNiveis.text, out dados.qtdNiveis);

        dados.configurado = true;

        if(imagemBotaoAnterior != null)
        {
            imagemBotaoAnterior.color = corConfigurado;
        }

        FecharPainelPrateleira();

    }

    public void FecharPainelPrateleira ()
    {
        if(setorSelecionadoAtual != null && imagemBotaoAnterior != null)
        {
            if(bancoDeDadosSetores.ContainsKey(setorSelecionadoAtual) && bancoDeDadosSetores[setorSelecionadoAtual].configurado)
            {
                imagemBotaoAnterior.color = corConfigurado;
            }
            else
            {
                imagemBotaoAnterior.color = corNormal;
            }
        }

        setorSelecionadoAtual = null;
        imagemBotaoAnterior = null;
        botaoRemove.SetActive(false);

        if(painelConfigPrateleira != null) painelConfigPrateleira.SetActive(false);
    }

    public void ClicouRemover()
    {
        if(setorSelecionadoAtual != null)
        {
            bancoDeDadosSetores.Remove(setorSelecionadoAtual);
            Destroy(setorSelecionadoAtual);

            setorSelecionadoAtual = null;
            imagemBotaoAnterior = null;

            botaoRemove.SetActive(false);
            if(painelConfigPrateleira != null) painelConfigPrateleira.SetActive(false);
        }
    }
}
