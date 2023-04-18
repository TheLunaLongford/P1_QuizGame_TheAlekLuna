using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class pregunta
{
    public string texto ;
    public respuesta[] arreglo_respuestas = new respuesta[4];
    public int valor_pregunta;
    public int dificultad;     // [1: Easy, 2: Normal, 3:Difícil, 4: Random]
}
