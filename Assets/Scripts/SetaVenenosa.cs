using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetaVenenosa : MonoBehaviour
{
    public float cantidadVeneno;
    public float indiceDeterioro; // Tiempo entre tick de veneno
    private List<IDeterioro> listaObjetosDeterioro = new List<IDeterioro>(); // Lista de cosas que se pueden ver afectadas por el veneno
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ManejarVeneno()); // Inicia la coroutine que controla el veneno
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ManejarVeneno()
    {
        while (true) // Bucle infinito para que el veneno se mantenga activo
        {
            for (int i = 0; i < listaObjetosDeterioro.Count; i++) // Recorre la lista de objetos que se ven afectados por el veneno
            {
                listaObjetosDeterioro[i].ProducirDeterioro(cantidadVeneno); // Aplica el veneno al objeto
            }
        yield return new WaitForSeconds(indiceDeterioro); // Espera el tiempo entre ticks de veneno
        }

    }
   private void  OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<IDeterioro>() != null) // Si el objeto no tiene el componente IDeterioro, no se añade a la lista
            listaObjetosDeterioro.Add(other.gameObject.GetComponent<IDeterioro>()); // Añade el objeto a la lista de objetos que se ven afectados por el veneno
    }
    private void  OnCollisionExit(Collision other)
    {
                if (other.gameObject.GetComponent<IDeterioro>() != null) // Si el objeto no tiene el componente IDeterioro, no se elimina a la lista
            listaObjetosDeterioro.Remove(other.gameObject.GetComponent<IDeterioro>()); // Elimina el objeto a la lista de objetos que se ven afectados por el veneno

    }
}
