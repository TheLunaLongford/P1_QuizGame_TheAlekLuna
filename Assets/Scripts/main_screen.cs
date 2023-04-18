using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main_screen : MonoBehaviour
{
    // UI Elements
    public Canvas pantalla_principal;
    public Canvas pantalla_juego;
    // public List<pregunta> mi_pregunta = new List<pregunta>();

    // Data Elements
    public TextAsset archivo_easy;
    public TextAsset archivo_medium;
    public TextAsset archivo_hard;

    string texto_preguntas_easy;
    string texto_preguntas_medium;
    string texto_preguntas_hard;
    public void cargar_preguntas()
    {
        texto_preguntas_easy = archivo_easy.text;
        texto_preguntas_medium = archivo_medium.text;
        texto_preguntas_hard = archivo_hard.text;
        if (texto_preguntas_easy != "" & texto_preguntas_medium != "" & texto_preguntas_hard != "")
        {
            // Debug.Log(texto_preguntas);
            json_2_objetos();
        }
        else
        {
            Debug.Log("no existe la wea, favor de agregar el registro de texto");
        }
    }

    void json_2_objetos()
    {
        //pregunta nueva_pregunta = JsonUtility.FromJson<pregunta>(texto_preguntas);
        //DumpToConsole(mi_pregunta);
        cuestionario cuestionario_easy = JsonUtility.FromJson<cuestionario>(texto_preguntas_easy);
        DumpToConsole(cuestionario_easy);
        cuestionario cuestionario_medium = JsonUtility.FromJson<cuestionario>(texto_preguntas_medium);
        DumpToConsole(cuestionario_medium);
        cuestionario cuestionario_hard = JsonUtility.FromJson<cuestionario>(texto_preguntas_hard);
        DumpToConsole(cuestionario_hard);
    }


    // Start is called before the first frame update
    void Start()
    {
        cargar_preguntas();
        //json_2_objetos();
    }
    public static void DumpToConsole(cuestionario p)
    {
        var output = JsonUtility.ToJson(p, true);
        Debug.Log(output);
    }
}
