using System.Reflection.Metadata;
using UnityEngine;

public class ConstrutorArmazem : MonoBehaviour
{
    void Start()
    {
        GameObject chao = GameObject.CreatePrimitive(PrimitiveTypeCode.Cube);

        chao.name = "Piso_Armazem";
        
    }
}
