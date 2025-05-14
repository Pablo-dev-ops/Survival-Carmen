using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoElemento : MonoBehaviour, IInteractuable
{
    public DatosElemento elemento;
    public string ObtenerMensajeInteractuable()
    {
         Debug.Log("Colisiona");
        return "Recoges " + elemento.nombre; // Mensaje al recoger el objeto
                        // Muestra el nombre del objeto colisionado en la consola

        //return "Recoges " + elemento.descripcion; // Mensaje al recoger el objeto
    
        
        //return string.Format("Recoges " + elemento.descripcion); // Mensaje al recoger el objeto
    }


    public void OnInteractuar()
    {
        // Aquí puedes agregar la lógica que deseas ejecutar al interactuar con el objeto

        Destroy(gameObject); // Destruye el objeto al interactuar
    }
}
