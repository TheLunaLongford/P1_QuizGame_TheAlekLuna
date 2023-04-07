using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class click_start : MonoBehaviour
{
    public TextMeshProUGUI boton_start;

    public void clickeado()
    {
        boton_start.text = "VAMOS!!!";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
