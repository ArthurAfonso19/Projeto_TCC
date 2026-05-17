using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class MenuConfig3D : MonoBehaviour 
{
    [Header("Painéis de Configuração")]
    [Tooltip("O painel onde digita Largura, Comprimento e Altura")]
    public GameObject panelWarehouseConfig; 
    
    [Tooltip("O novo painel de criação de Setores e Prateleiras")]
    public GameObject panelShelfConfigurations; 

    [Header("Inputs do Armazém")]
    public TMP_InputField inputLargura;
    public TMP_InputField inputComprimento;
    public TMP_InputField inputAltura;

    public void AvancarConfigSetores()
    {
        // 1. Salva os dados físicos básicos
        float.TryParse(inputLargura.text, out DataManager.Largura);
        float.TryParse(inputComprimento.text, out DataManager.Comprimento);
        float.TryParse(inputAltura.text, out DataManager.Altura);

        if(panelWarehouseConfig != null) panelWarehouseConfig.SetActive(false);
        if(panelShelfConfigurations != null) panelShelfConfigurations.SetActive(true);
    }

    public void VoltarParaConfigArmazem()
    {
        if(panelShelfConfigurations != null) panelShelfConfigurations.SetActive(false);
        if(panelWarehouseConfig != null) panelWarehouseConfig.SetActive(true);
    }

    public void FinalizaECarrega3D()
    {
        SceneManager.LoadScene("CenaArmazem");
    }
}