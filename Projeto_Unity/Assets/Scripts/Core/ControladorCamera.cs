using System;
using UnityEngine;
// IMPORTANTE: Precisamos importar o novo sistema de Input
using UnityEngine.InputSystem; 

public class ControladorCamera : MonoBehaviour
{
    private float velocidadeZoom = 150f;
    private float velocidadeMovimento;
    private float velocidadeRotacao = 0.1f; // Ajustado pois o delta do novo mouse é baseado em pixels

    private float pitch = 0f;
    private float yaw = 0f;

    void Start()
    {
        float largura = DataManager.Largura;
        float comprimento = DataManager.Comprimento;
        float altura = DataManager.Altura;

        float maiorLado = Math.Max(largura, comprimento);
        Camera.main.farClipPlane = maiorLado * 10f;

        float distanciaDiagonal = maiorLado * 1.5f; 
        float alturaCamera = Math.Max(distanciaDiagonal, altura * 2.0f);

        transform.position = new Vector3(distanciaDiagonal, alturaCamera, -distanciaDiagonal);
        transform.LookAt(Vector3.zero);

        velocidadeMovimento = maiorLado * 0.6f;
        velocidadeZoom = maiorLado * 0.4f;

        pitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;
    }

    void Update()
    {
        // Proteção caso o teclado ou mouse não sejam detectados
        if (Keyboard.current == null || Mouse.current == null) return;

        // 1. ZOOM (Scroll da Roleta)
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll != 0.0f)
        {
            // O novo sistema retorna valores altos (ex: 120 ou -120), então normalizamos para 1 ou -1
            float direcaoScroll = Mathf.Clamp(scroll, -1f, 1f);
            transform.position += transform.forward * direcaoScroll * velocidadeZoom * Time.deltaTime * 10f;
        }

        // 2. ROTAÇÃO (Clicar e segurar o botão do meio/roleta do mouse)
        if (Mouse.current.middleButton.isPressed)
        {
            // Pega o deslocamento do mouse em pixels
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            
            yaw += mouseDelta.x * velocidadeRotacao;
            pitch -= mouseDelta.y * velocidadeRotacao;

            // Mantém os ângulos sob controle
            pitch = Mathf.Clamp(pitch, -80f, 80f);

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }

        // 3. MOVIMENTO LIVRE (Teclas W, A, S, D)
        float moverH = 0f;
        if (Keyboard.current.dKey.isPressed) moverH += 1f;
        if (Keyboard.current.aKey.isPressed) moverH -= 1f;

        float moverV = 0f;
        if (Keyboard.current.wKey.isPressed) moverV += 1f;
        if (Keyboard.current.sKey.isPressed) moverV -= 1f;

        // Aplica o movimento puramente na direção em que a câmera está olhando no espaço 3D
        Vector3 vetorMovimento = (transform.forward * moverV + transform.right * moverH) * velocidadeMovimento * Time.deltaTime;
        transform.position += vetorMovimento;
    }
}