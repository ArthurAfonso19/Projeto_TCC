using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class MenuConfig3D : MonoBehaviour 
{
    public TMP_InputField inputLargura;
    public TMP_InputField inputComprimento;
    public TMP_InputField inputAltura;

    public void SalvarEIniciar()
    {
        // Alterado de DataViewManager para DataManager
        float.TryParse(inputLargura.text, out DataManager.Largura);
        float.TryParse(inputComprimento.text, out DataManager.Comprimento);
        float.TryParse(inputAltura.text, out DataManager.Altura);

        // Forçando o uso do Debug específico da Unity para evitar o erro de ambiguidade
        UnityEngine.Debug.Log($"Dados Salvos: L:{DataManager.Largura} C:{DataManager.Comprimento} A:{DataManager.Altura}");

        SceneManager.LoadScene("CenaArmazem"); 
    }
}