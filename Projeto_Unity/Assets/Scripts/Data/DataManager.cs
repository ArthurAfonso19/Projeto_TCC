using System;
using UnityEngine;

public class DataManager
{
    public static float Largura;
    public static float Comprimento;
    public static float Altura;

    private const string LISTA_SAVES_KEY = "ListaDeArmazens";

    public static void SalvarDados(String nomeDoSave)
    {
        PlayerPrefs.SetFloat(nomeDoSave + "_Largura", Largura);
        PlayerPrefs.SetFloat(nomeDoSave + "_Comprimento", Comprimento);
        PlayerPrefs.SetFloat(nomeDoSave + "_Altura", Altura);

        string listaAtual = PlayerPrefs.GetString(LISTA_SAVES_KEY, "");
        if(!listaAtual.Contains(nomeDoSave))
        {
            listaAtual += (listaAtual == "" ? "" : "|") + nomeDoSave;
            PlayerPrefs.SetString(LISTA_SAVES_KEY, listaAtual);
        }

        PlayerPrefs.Save();
        Debug.Log("Armazém " + nomeDoSave + " salvo com sucesso!");
    }

    public static void CarregarDados(string nomeDoSave)
    {
        Largura = PlayerPrefs.GetFloat(nomeDoSave + "_Largura");
        Comprimento = PlayerPrefs.GetFloat(nomeDoSave + "_Comprimento");
        Altura = PlayerPrefs.GetFloat(nomeDoSave + "_Altura");
    }

    public static string[] GetListadeSaves()
    {
        string lista = PlayerPrefs.GetString(LISTA_SAVES_KEY, "");
        if(string.IsNullOrEmpty(lista)) return new string[0];
        return lista.Split('|');
    }
}
