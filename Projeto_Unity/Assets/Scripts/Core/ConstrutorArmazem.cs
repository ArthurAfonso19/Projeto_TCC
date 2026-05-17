//using System.Numerics;
using System;
using UnityEngine;
public class ConstrutorArmazem : MonoBehaviour
{
   void Awake()
    {
        float largura = DataManager.Largura; 
        float comprimento = DataManager.Comprimento;
        float altura = DataManager.Altura;
        float espessura = Mathf.Max(0.5f, largura * 0.01f);
        
        //Criar o chão 
        GameObject chao = GameObject.CreatePrimitive(PrimitiveType.Cube);
        chao.name = "Piso";
        chao.transform.localScale = new Vector3(largura, 0.1f, comprimento);
        chao.transform.position = Vector3.zero;
        chao.GetComponent<Renderer>().material.color = new Color(0.2f, 0.2f, 0.2f);

        //Truque visual
        Color corParedesFundoFrente = new Color(0.9f, 0.9f, 0.9f); // Quase branco
        Color corParedesLaterais = new Color(0.7f, 0.7f, 0.7f); // Cinza claro

        //Criar as paredes 
        CriarParede("Parede_Fundo", new Vector3(0, altura / 2, comprimento / 2), new Vector3(largura, altura, espessura), corParedesFundoFrente);
        CriarParede("Parede_Frente", new Vector3(0, altura / 2, -comprimento / 2), new Vector3(largura, altura, espessura), corParedesFundoFrente);
        CriarParede("Parede_Esquerda", new Vector3(-largura / 2, altura / 2, 0), new Vector3(espessura, altura, comprimento), corParedesLaterais);
        CriarParede("Parede_Direita", new Vector3(largura / 2, altura / 2, 0), new Vector3(espessura, altura, comprimento), corParedesLaterais);

    }

   void CriarParede(String nome, Vector3 posicao, Vector3 escala, Color corDaParede)
    {
        GameObject parede = GameObject.CreatePrimitive(PrimitiveType.Cube);
        parede.name = nome;
        parede.transform.position = posicao;
        parede.transform.localScale = escala;

        // Aplica a cor que foi enviada
        parede.GetComponent<Renderer>().material.color = corDaParede;
    }
}
