using UnityEngine;

public class SairMenuInicial : MonoBehaviour
{
    public void SairJogo()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
