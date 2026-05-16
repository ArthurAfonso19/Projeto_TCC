using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GerenciadorLoad : MonoBehaviour
{
    [SerializeField] private GameObject painelLoad; 
    [SerializeField] private GameObject botaoSavePrefab; 
    [SerializeField] private Transform containerBotoes; 

    public void AbrirPainelLoad()
    {
        painelLoad.SetActive(true);

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
            novoBotao.GetComponent<Button>().onClick.AddListener(() => CarregarEsteArmazem(nomeTemporario));
        }
    }

    private void CarregarEsteArmazem(string nome)
    {
        Time.timeScale = 1f;
        DataManager.CarregarDados(nome);
        SceneManager.LoadScene("CenaArmazem"); 
    }

}
