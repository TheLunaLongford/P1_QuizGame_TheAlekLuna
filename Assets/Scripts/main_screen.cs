using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class main_screen : MonoBehaviour
{
    // UI Elements : Canvas
    public Canvas pantalla_principal;
    public Canvas pantalla_juego;
    public Canvas pantalla_dificultad;
    public Canvas pantalla_score;

    // UI Elements : Buttons
    public Button button_res_1;
    public Button button_res_2;
    public Button button_res_3;
    public Button button_res_4;

    // Data Elements
    public TextAsset archivo_easy;
    public TextAsset archivo_medium;
    public TextAsset archivo_hard;

    string texto_preguntas_easy;
    string texto_preguntas_medium;
    string texto_preguntas_hard;

    cuestionario cuestionario_easy;
    cuestionario cuestionario_medium;
    cuestionario cuestionario_hard;

    cuestionario quiz;
    int actual_question_numero;

    // TMP Elements
    public TextMeshProUGUI pregunta;
    public TextMeshProUGUI res_1;
    public TextMeshProUGUI res_2;
    public TextMeshProUGUI res_3;
    public TextMeshProUGUI res_4;

    // Score Elements
    int puntuacion_juego_actual;
    int pregunta_actual;
    public TextMeshProUGUI puntuacion;
    public TextMeshProUGUI puntuacion_final;
    public TextMeshProUGUI record;

    // Additional stuff
    public Button dummy;
    bool segunda_oportunidad;
    double multiplicador_puntuacion;
    public Button omitir;
    public Button terminar;
    public TextMeshProUGUI new_record;
    public Image character;
    float tiempo_restante;
    bool tiempo_bandera;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI record_main;
    public Button pista;


    public void seleccion_dificultad()
    {
        pantalla_juego.gameObject.SetActive(false);
        pantalla_principal.gameObject.SetActive(false);
        pantalla_dificultad.gameObject.SetActive(true);
        pantalla_score.gameObject.SetActive(false);
    }
    public void mostrar_puntuacion()
    {
        pantalla_juego.gameObject.SetActive(false);
        pantalla_principal.gameObject.SetActive(false);
        pantalla_dificultad.gameObject.SetActive(true);
        pantalla_score.gameObject.SetActive(true);

        int record_puntos = PlayerPrefs.GetInt("record", 0);
        if (puntuacion_juego_actual > record_puntos)
        {
            PlayerPrefs.SetInt("record", puntuacion_juego_actual);
            record_puntos = puntuacion_juego_actual;
            new_record.alpha = 1.0f;
        }
        puntuacion_final.SetText(puntuacion_juego_actual.ToString());
        record.SetText(record_puntos.ToString());
    }

    public void gastar_pista()
    {
        if (puntuacion_juego_actual >= 300)
        {
            int pistas_eliminadas = 0;
            int boton_random = 0;
            do
            {
                boton_random = Random.Range(1, 4);
                switch (boton_random)
                {
                    case 1:
                        if (button_res_1.interactable == true & button_res_1.GetComponent<data_pregunta>().correcto == false)
                        {
                            pistas_eliminadas++;
                            button_res_1.interactable = false;
                        }
                        break;
                    case 2:
                        if (button_res_2.interactable == true & button_res_2.GetComponent<data_pregunta>().correcto == false)
                        {
                            pistas_eliminadas++;
                            button_res_2.interactable = false;
                        }
                        break;
                    case 3:
                        if (button_res_3.interactable == true & button_res_3.GetComponent<data_pregunta>().correcto == false)
                        {
                            pistas_eliminadas++;
                            button_res_3.interactable = false;
                        }
                        break;
                    case 4:
                        if (button_res_4.interactable == true & button_res_4.GetComponent<data_pregunta>().correcto == false)
                        {
                            pistas_eliminadas++;
                            button_res_4.interactable = false;
                        }
                        break;
                }
            } while (pistas_eliminadas < 2);
            puntuacion_juego_actual -= 300;
            puntuacion_final.SetText(puntuacion_juego_actual.ToString());
        }
        

    }

    public void comenzar_juego()
    {
        pantalla_juego.gameObject.SetActive(true);
        pantalla_principal.gameObject.SetActive(false);
        pantalla_dificultad.gameObject.SetActive(false);
        pantalla_score.gameObject.SetActive(false);

        // Dependiente de que difultad fue elegida, se cargara un archivo de preguntas u otro
        string button_name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (button_name)
        {
            case "button_easy":
                quiz = cuestionario_easy;
                break;

            case "button_medium":
                quiz = cuestionario_medium;
                break;

            case "button_hard":
                quiz = cuestionario_hard;
                break;
        }

        quiz = shuffle_quiz(quiz);
        // Setear valores de inicio de juego
        actual_question_numero = 0;
        puntuacion_juego_actual = 0;
        puntuacion.SetText(puntuacion_juego_actual.ToString());

        poner_pregunta();
        //DumpToConsole(quiz);
    }

    public void poner_pregunta()
    {
        dummy.Select();
        desbloquear_botones();
        tiempo_bandera = true;
        tiempo_restante = 11;
        if (actual_question_numero < 10)
        {
            pregunta pregunta_random = quiz.arreglo_preguntas[actual_question_numero];
            int numero_respuestas = pregunta_random.arreglo_respuestas.Length;
            int[] indices = shuffle_range(numero_respuestas);

            llenar_pregunta(pregunta_random, indices);
            ajustar_colores_botones(indices);
            centrar_botones();
            ubicar_botones();
            navi_color(1);
            segunda_oportunidad = true;
            actual_question_numero += 1;
            multiplicador_puntuacion = 1;
        }
        else
        {
            mostrar_puntuacion();
        }
    }

    public void navi_color(int color)
    {
        Color navi_color = new Color();
        navi_color.a = 1;
        switch (color)
        {
            // Neutral
            case 1:
                navi_color.r = 255;
                navi_color.g = 255;
                navi_color.b = 255;
                character.color = navi_color;
                break;

            // Enojado
            case 2:
                navi_color.r = 255;
                navi_color.g = 0;
                navi_color.b = 0;
                character.color = navi_color;
                break;

            // Feliz
            case 3:
                navi_color.r = 0;
                navi_color.g = 255;
                navi_color.b = 0;
                character.color = navi_color;
                break;
        }
        
    }

    public void click_respuesta()
    {
        GameObject boton_clickeado = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (boton_clickeado.GetComponent<data_pregunta>().correcto)
        {
            puntuacion_juego_actual +=  (int)((double)boton_clickeado.GetComponent<data_pregunta>().valor_puntos * multiplicador_puntuacion);
            puntuacion.SetText(puntuacion_juego_actual.ToString());
            bloquear_botones();
            navi_color(3);
            tiempo_bandera = false;
            Invoke("poner_pregunta", 2);
        }
        else
        {
            tiempo_restante = 11;
            navi_color(2);
            if (segunda_oportunidad)
            {
                segunda_oportunidad = false;
                boton_clickeado.GetComponent<Button>().interactable = false;
                multiplicador_puntuacion = 0.25;
            }
            else
            {
                multiplicador_puntuacion = -0.25;
                puntuacion_juego_actual += (int)((double)boton_clickeado.GetComponent<data_pregunta>().valor_puntos * multiplicador_puntuacion);
                puntuacion.SetText(puntuacion_juego_actual.ToString());
                bloquear_botones();
                tiempo_bandera = false;
                Invoke("poner_pregunta", 2);
            }
        }
    }

    public void bloquear_botones()
    {
        button_res_1.interactable = false;
        button_res_2.interactable = false;
        button_res_3.interactable = false;
        button_res_4.interactable = false;
        omitir.interactable = false;
        pista.interactable = false;
    }

    public void desbloquear_botones()
    {
        button_res_1.interactable = true;
        button_res_2.interactable = true;
        button_res_3.interactable = true;
        button_res_4.interactable = true;
        omitir.interactable = true;
        pista.interactable = true;
    }

    public void llenar_pregunta(pregunta pregunta_en_turno, int[] orden)
    {
        pregunta.SetText(pregunta_en_turno.texto);
        res_1.SetText(pregunta_en_turno.arreglo_respuestas[orden[0] - 1].texto);        
        res_2.SetText(pregunta_en_turno.arreglo_respuestas[orden[1] - 1].texto);        
        res_3.SetText(pregunta_en_turno.arreglo_respuestas[orden[2] - 1].texto);
        res_4.SetText(pregunta_en_turno.arreglo_respuestas[orden[3] - 1].texto);
        
        button_res_1.GetComponent<data_pregunta>().correcto = pregunta_en_turno.arreglo_respuestas[orden[0] - 1].correct;
        button_res_2.GetComponent<data_pregunta>().correcto = pregunta_en_turno.arreglo_respuestas[orden[1] - 1].correct;
        button_res_3.GetComponent<data_pregunta>().correcto = pregunta_en_turno.arreglo_respuestas[orden[2] - 1].correct;
        button_res_4.GetComponent<data_pregunta>().correcto = pregunta_en_turno.arreglo_respuestas[orden[3] - 1].correct;

        button_res_1.GetComponent<data_pregunta>().valor_puntos = pregunta_en_turno.valor_pregunta;
        button_res_2.GetComponent<data_pregunta>().valor_puntos = pregunta_en_turno.valor_pregunta;
        button_res_3.GetComponent<data_pregunta>().valor_puntos = pregunta_en_turno.valor_pregunta;
        button_res_4.GetComponent<data_pregunta>().valor_puntos = pregunta_en_turno.valor_pregunta;
    }

    public void ajustar_colores_botones(int[] indices)
    {
        colores_por_default_();
        set_colores_respuestas(indices);
    }

    public Color get_colores_respuestas(int id_respuesta)
    {
        bool es_correcto = quiz.arreglo_preguntas[actual_question_numero].arreglo_respuestas[id_respuesta].correct;
        if (es_correcto)
        {
            return Color.green;
        }
        return Color.red;
    }

    public void set_colores_respuestas(int[] indices)
    {
        Color c;
        ColorBlock cb;
        cb = button_res_1.colors;
        c = get_colores_respuestas(indices[0] - 1);
        cb.disabledColor = c;
        button_res_1.colors = cb;

        cb = button_res_2.colors;
        c = get_colores_respuestas(indices[1] - 1);
        cb.disabledColor = c;
        button_res_2.colors = cb;

        cb = button_res_3.colors;
        c = get_colores_respuestas(indices[2] - 1);
        cb.disabledColor = c;
        button_res_3.colors = cb;

        cb = button_res_4.colors;
        c = get_colores_respuestas(indices[3] - 1);
        cb.disabledColor = c;
        button_res_4.colors = cb;
    }

    public void colores_por_default_()
    {
        ColorBlock cb;
        cb = button_res_1.colors;
        cb.disabledColor = Color.gray;
        button_res_1.colors = cb;

        cb = button_res_2.colors;
        cb.disabledColor = Color.gray;
        button_res_2.colors = cb;

        cb = button_res_3.colors;
        cb.disabledColor = Color.gray;
        button_res_3.colors = cb;

        cb = button_res_4.colors;
        cb.disabledColor = Color.gray;
        button_res_4.colors = cb;
    }

    public void centrar_botones()
    {
        pantalla_juego.transform.GetChild(2).Translate(-200.0f, -40.0f, 0.0f, Space.World);
        pantalla_juego.transform.GetChild(3).Translate(200.0f, -40.0f, 0.0f, Space.World);
        pantalla_juego.transform.GetChild(4).Translate(-200.0f, -100.0f, 0.0f, Space.World);
        pantalla_juego.transform.GetChild(5).Translate(200.0f, -100.0f, 0.0f, Space.World);
    }

    public void ubicar_botones()
    {
        pantalla_juego.transform.GetChild(2).Translate(200.0f, 40.0f, 0.0f, Space.World);
        pantalla_juego.transform.GetChild(3).Translate(-200.0f, 40.0f, 0.0f, Space.World);
        pantalla_juego.transform.GetChild(4).Translate(200.0f, 100.0f, 0.0f, Space.World);
        pantalla_juego.transform.GetChild(5).Translate(-200.0f, 100.0f, 0.0f, Space.World);
    }

    public cuestionario shuffle_quiz(cuestionario quiz)
    {
        int numero_preguntas = quiz.arreglo_preguntas.Length;
        int[] indices = shuffle_range(numero_preguntas);

        // Vaciar preguntas mezcladas
        cuestionario shuffle_quiz = new cuestionario();
        for (int i = 0; i<= numero_preguntas-1; i++)
        {
            shuffle_quiz.arreglo_preguntas[i] = quiz.arreglo_preguntas[indices[i]-1]; 
        }

        return shuffle_quiz;
    }

    public void regresar_home()
    {
        // Reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void inicializar_pantallas()
    {
        pantalla_juego.gameObject.SetActive(false);
        pantalla_principal.gameObject.SetActive(true);
        pantalla_dificultad.gameObject.SetActive(false);
        pantalla_score.gameObject.SetActive(false);
        // Setting record value to 0 when starting
        record_main.SetText(PlayerPrefs.GetInt("record", 0).ToString());
        // Descomentar si quremos que se borre, TODO: agregarlo como boton.
        // PlayerPrefs.SetInt("record", puntuacion_juego_actual);
        new_record.alpha = 0.0f;
    }

    void Start()
    {
        cargar_preguntas();
        inicializar_pantallas();
        tiempo_bandera = false;
    }

    void Update()
    {
        if (tiempo_bandera)
        {
            if(tiempo_restante >= 1)
            {
                tiempo_restante -= Time.deltaTime;
                actualizar_timer();
            }
            else
            {
                bloquear_botones();
                navi_color(2);
                tiempo_bandera = false;
                Invoke("poner_pregunta", 2);
            }
        }
       
    }

    public void actualizar_timer()
    {
        float segundos = Mathf.FloorToInt(tiempo_restante % 60);
        timer.SetText(string.Format("00:{0:00}", segundos));
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
        cuestionario_easy = JsonUtility.FromJson<cuestionario>(texto_preguntas_easy);
        // DumpToConsole(cuestionario_easy);
        cuestionario_medium = JsonUtility.FromJson<cuestionario>(texto_preguntas_medium);
        //DumpToConsole(cuestionario_medium);
        cuestionario_hard = JsonUtility.FromJson<cuestionario>(texto_preguntas_hard);
        //DumpToConsole(cuestionario_hard);
    }

    // Utilities
    public static void DumpToConsole(cuestionario p)
    {
        var output = JsonUtility.ToJson(p, true);
        Debug.Log(output);
    }

    public int[] shuffle_range(int longitud)
    {
        int[] indices = new int[longitud];
        // Obtener el rango de 0 a 9
        for (int i = 1; i <= longitud; i++)
        {
            indices[i - 1] = i;
        }
        // Mezclando los elementos del rango
        for (int i = longitud; i >= 1; i--)
        {
            int j = Random.Range(0, i);
            int aux = indices[j];
            indices[j] = indices[i - 1];
            indices[i - 1] = aux;
        }
        return indices;
    }
}
