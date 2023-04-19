using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main_screen : MonoBehaviour
{
    // UI Elements : Canvas
    public Canvas pantalla_principal;
    public Canvas pantalla_juego;
    public Canvas pantalla_dificultad;
    public Canvas pantalla_score;

    // UI Elements : Buttons
    // public GameObject boton_empezar;


    // Data Elements
    public TextAsset archivo_easy;
    public TextAsset archivo_medium;
    public TextAsset archivo_hard;

    string texto_preguntas_easy;
    string texto_preguntas_medium;
    string texto_preguntas_hard;

    public void comenzar_juego()
    {
        pantalla_juego.gameObject.SetActive(true);
        pantalla_principal.gameObject.SetActive(false);
        pantalla_dificultad.gameObject.SetActive(false);
        //pantalla_score.gameObject.SetActive(false);
    }
    public void regresar_home()
    {
        pantalla_juego.gameObject.SetActive(false);
        pantalla_principal.gameObject.SetActive(true);
        pantalla_dificultad.gameObject.SetActive(false);
        //pantalla_score.gameObject.SetActive(false);
    }
    public void seleccion_dificultad()
    {
        pantalla_juego.gameObject.SetActive(false);
        pantalla_principal.gameObject.SetActive(false);
        pantalla_dificultad.gameObject.SetActive(true);
        //pantalla_score.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        cargar_preguntas();
        regresar_home();
    }
    public void cargar_preguntas()
    {
        texto_preguntas_easy = archivo_easy.text;
        texto_preguntas_medium = archivo_medium.text;
        texto_preguntas_hard = archivo_hard.text;
        if (texto_preguntas_easy != "" & texto_preguntas_medium != "" & texto_preguntas_hard != "")
        {
            json_2_objetos();
        }
        else
        {
            Debug.Log("no existe la wea, favor de agregar el registro de texto");
        }
    }

    void json_2_objetos()
    {
        cuestionario cuestionario_easy = JsonUtility.FromJson<cuestionario>(texto_preguntas_easy);
        DumpToConsole(cuestionario_easy);
        cuestionario cuestionario_medium = JsonUtility.FromJson<cuestionario>(texto_preguntas_medium);
        DumpToConsole(cuestionario_medium);
        cuestionario cuestionario_hard = JsonUtility.FromJson<cuestionario>(texto_preguntas_hard);
        DumpToConsole(cuestionario_hard);
    }

    // Utilities
    public static void DumpToConsole(cuestionario p)
    {
        var output = JsonUtility.ToJson(p, true);
        Debug.Log(output);
    }
}
