using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boton_empezar : MonoBehaviour
{
    // UI Elements
    public Canvas pantalla_principal;
    public Canvas pantalla_juego;
    public Button boton_comenzar;
    // Start is called before the first frame update

    public void comenzar_juego()
    {
        pantalla_principal.gameObject.SetActive(false);
        pantalla_juego.gameObject.SetActive(true);
    }
}
