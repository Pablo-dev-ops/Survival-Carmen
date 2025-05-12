using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInteraccion : MonoBehaviour
{
   

}
public interface IInteractuable
    {
        string ObtenerMensajeInteractuable(); //Para el mensaje al recoger
        void OnInteractuar(); // Método para interactuar con el objeto
    }
