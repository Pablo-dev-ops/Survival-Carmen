using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ElementoEstanteriaUI : MonoBehaviour
{
    public Button botonElemento; // Botón para el elemento
    public TextMeshProUGUI textoElemento; // Texto para el nombre del elemento
    public Image imagenElemento; // Imagen para el sprite del elemento
    public int indice;
    private ElementoEstanteria elementoActual;


public void Establecer(ElementoEstanteria elemento)
    {
        // Asigna el elemento actual
        elementoActual = elemento;

        // Establece el sprite del botón
        imagenElemento.enabled = true; // Asegúrate de que la imagen esté activa
        imagenElemento.sprite = elemento.elemento.icono;
        // Establece el texto del botón
        textoElemento.text = elemento.cantidad>1 ? elemento.cantidad.ToString() :string.Empty; // Muestra la cantidad si es mayor a 1

    }

    public void Limpiar()
    {
        elementoActual = null;

        //no se elimina el elemento, se desactiva para poder reutilizarlo
        imagenElemento.enabled = false; // Desactiva la imagen
        textoElemento.text = ""; // Limpia el texto
    }

    public void OnButtonClick()
    {
        ControlInventario.instancia.ElementoSeleccionado(indice); // Llama al método ElementoSeleccionado en ControlInventario
    }

    // Start is called before the first frame update
    void Start()
    {
        botonElemento = this.GetComponentInChildren<Button>(); // Obtiene el componente Button del objeto hijo
        botonElemento.image.sprite = null; // Inicializa el sprite del botón como nulo
        botonElemento.GetComponentInChildren<TextMeshProUGUI>().text = ""; // Inicializa el texto del botón como vacío
    }

    // Update is called once per frame
    void Update()
    {

    }
}
