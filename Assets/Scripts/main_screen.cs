using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class main_screen : MonoBehaviour
{
    // UI Elements
    public Canvas pantalla_principal;
    public Canvas pantalla_juego;

    // Data Elements
    public TextAsset archivo_preguntar;

    public void cargar_preguntas()
    {
        string texto_preguntas = archivo_preguntar.text;
        if (texto_preguntas != "")
        {
            Debug.Log(texto_preguntas);
        }
        else
        {
            Debug.Log("no existe la wea");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        cargar_preguntas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
