using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
public class GerenciadoMenuPausa : MonoBehaviour
{
    [Header("Configurações de UI - Menu Principal")]
    [SerializeField] private GameObject menuPausaPanel;

    [Header("Configurações de UI - Painel Save")]
    [SerializeField] private GameObject painelNomeSave;
    [SerializeField] private TMP_InputField inputNomeSave;

    private void Start()
    {
        if(menuPausaPanel != null) menuPausaPanel.SetActive(false);
        if(painelNomeSave != null) painelNomeSave.SetActive(false);
    }

    private void Update()
    {
        if(Keyboard.current == null) return;

        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (painelNomeSave != null && painelNomeSave.activeSelf)
            {
                ClicouVoltarSave();
                return;
            }
            else
            {
                AlternarMenu();
            }
        }
    }

    public void AlternarMenu()
    {
        if(menuPausaPanel != null)
        {
            bool estadoAtual = menuPausaPanel.activeSelf;
            menuPausaPanel.SetActive(!estadoAtual);

            if(estadoAtual == true && painelNomeSave != null)
            {
                painelNomeSave.SetActive(false);
            }
            Time.timeScale = estadoAtual ? 1f : 0f;
        }
    }

    public void ClicouRetomar()
    {
        AlternarMenu();
    }

    public void ClicouSalvar()
    {
        if(painelNomeSave != null)
        {
            painelNomeSave.SetActive(true);
            if(inputNomeSave != null) inputNomeSave.text = "";
        }
    }
    public void ClicouVoltarSave()
    {
        if(painelNomeSave != null)
        {
            painelNomeSave.SetActive(false);
        }
    }

    public void ClicouConfirmaSave()
    {
        if(inputNomeSave != null && !string.IsNullOrEmpty(inputNomeSave.text))
        {
            string nomeDoSave = inputNomeSave.text;
            DataManager.SalvarDados(nomeDoSave);
            ClicouVoltarSave();
        }
    }

    public void ClicouSairMenuInicial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicial");
    }
    public void ClicouSair()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
