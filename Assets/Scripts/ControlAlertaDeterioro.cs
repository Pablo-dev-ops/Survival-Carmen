using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ControlAlertaDeterioro : MonoBehaviour
{
    public Image marcoRojo;
    public float velocidadDesaparecer;
    private Coroutine desaparecer;

    // Start is called before the first frame update
    void Start()
    {
         marcoRojo.color = new Color (1f, 1f, 1f, 0f); // Desactiva el marco rojo al inicio
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //aparece el marco rojo
    public void ApareceMarcoRojo()
    {
        if (desaparecer != null) // Si la corutina de desaparecer ya está en ejecución, la detenemos
        {
            StopCoroutine(desaparecer);
        }
        marcoRojo.enabled = true;
        marcoRojo.color = Color.white;
        desaparecer = StartCoroutine(Desaparecer());
        
        //marcoRojo.CrossFadeAlpha(1, velocidadDesaparecer, false);
    }
    //desaparece el marco rojo

    IEnumerator Desaparecer()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / velocidadDesaparecer;
            marcoRojo.color = new Color (1f, 1f, 1f, alpha);
            yield return null;
        }
        marcoRojo.enabled = false; // Desactiva el marco rojo al final de la corutina
    }
    //detiene la corutina de desaparecer
}
