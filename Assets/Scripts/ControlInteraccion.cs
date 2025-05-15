using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ControlInteraccion : MonoBehaviour
{
    private Camera camara;

    //Lanzar el rayo cada x segundos
    public float periodoChequeo = 0.5f; // Tiempo entre chequeos
    private float tiempoUltimoChequeo = 0f;
    public float distanciaChequeo = 0f; // Distancia máxima del chequeo
    public LayerMask capaChequeo; // Capa de los objetos a los que se puede interactuar

    //objetos a los que se puede interactuar
    private GameObject objetoInteractuable; // Objeto a interactuar
    private IInteractuable interactuable; // Interfaz para interactuar

    //texto
    public TextMeshProUGUI mensajeTexto; // Texto para mostrar el mensaje

    void Start()
    {
        camara = Camera.main; // Obtiene la cámara principal
        mensajeTexto.gameObject.SetActive(false); // Desactiva el objeto de texto al inicio

    }

    void Update()
    {
        //Lanzar el rayo cada x segundos
        if (Time.time >= tiempoUltimoChequeo + periodoChequeo)
        {
            tiempoUltimoChequeo = Time.time; // Actualiza el tiempo del último chequeo

            // Crea un rayo desde la cámara hacia el punto donde está mirando
            Ray rayo = camara.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            Debug.Log("Lanzando rayo"); // Muestra en la consola que se está lanzando el rayo
            // Si el rayo colisiona con un objeto
            if (Physics.Raycast(rayo, out hit, distanciaChequeo, capaChequeo))
            {
                Debug.Log("Colisiona con: " + hit.collider.gameObject.name); // Muestra el nombre del objeto colisionado en la consola
                if (hit.collider.gameObject != objetoInteractuable)
                {
                    objetoInteractuable = hit.collider.gameObject; // Obtiene el objeto colisionado
                    interactuable = objetoInteractuable.GetComponent<IInteractuable>(); // Obtiene el componente IInteractuable del objeto

                    EstablecerMensajeTexto(); // Establece el mensaje de texto
                }

            }
            else
            {
                mensajeTexto.gameObject.SetActive(false); // Desactiva el objeto de texto
                objetoInteractuable = null; // Si no hay colisión, establece el objeto a null
                interactuable = null; // Si no hay colisión, establece el interactuable a null
            }

        }
    }

    private void EstablecerMensajeTexto()
    {
        mensajeTexto.gameObject.SetActive(true); // Activa el objeto de texto
        Debug.Log("Colisiona"); // Muestra el nombre del objeto colisionado en la consola

        mensajeTexto.text = string.Format("<b>[E]</b> {0}", interactuable.ObtenerMensajeInteractuable()); // Establece el mensaje de texto
    }
}
public interface IInteractuable
{
    string ObtenerMensajeInteractuable(); //Para el mensaje al recoger
    void OnInteractuar(); // Método para interactuar con el objeto
}
