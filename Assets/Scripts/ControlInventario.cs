using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ControlInventario : MonoBehaviour
{
    //acceso a todos los elementos de la estanteria. separamos los gráficos de los datos de los elementos       
    public ElementoEstanteriaUI[] uiElementosEstanteria;
    public ElementoEstanteria[] elementosEstanteria;

    public Transform posicionSoltar;


    public GameObject ventanaInventario;


    [Header("Elemento seleccioando")]
    private ElementoEstanteria elementoSeleccionado;
    private int indiceElementoSeleccionado;
    public TextMeshProUGUI nombreElementoSeleccionado;
    public TextMeshProUGUI descripcionElementoSeleccionado;



    private ControlJugador controlJugador;
    public GameObject botonUsar;
    public GameObject botonSoltar;
    public static ControlInventario instancia;
    private ControlIndicador controladorIndicadores;

    void Awake()
    {
        instancia = this;
        controlJugador = GetComponent<ControlJugador>();
        controladorIndicadores = GetComponent<ControlIndicador>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ventanaInventario.SetActive(false);

        elementosEstanteria = new ElementoEstanteria[uiElementosEstanteria.Length];

        for (int i = 0; i < elementosEstanteria.Length; i++)
        {
            elementosEstanteria[i] = new ElementoEstanteria();
            uiElementosEstanteria[i].indice = i;
            uiElementosEstanteria[i].Limpiar();
        }

        //para que al arrancar el inventario
        LimpiarVentanaElementoSeleccionado();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AbrirCerrarVentanaInventario()
    {

        //Si la ventana está abierta, se cierra. So está cerrada, se abre
        if (ventanaInventario.activeInHierarchy)
        {
            ventanaInventario.SetActive(false);
            //onCerrarInventario.Invoke();
            //desactivo ratón y activo cam
            //controlJugador.PonerQuitarPunteroRaton(false);
        }
        else
        {
            ventanaInventario.SetActive(true);
            //onAbrirInventario.Invoke();
            LimpiarVentanaElementoSeleccionado();
            //controlJugador.PonerQuitarPunteroRaton(true);
        }

    }

    public bool EstaAbierto()
    {
        //Devuelve el valor booleano del estado de la ventana de inventario
        return ventanaInventario.activeInHierarchy;
    }

    // Método para añadir un elemento al inventario
    public void AnadirElemento(DatosElemento elemento)
    {
        ElementoEstanteria elementoParaAlmacenar = ObtenerElementoAlmacenado(elemento);
        if (elementoParaAlmacenar != null)
        {
            // Si el elemento ya existe en el inventario, actualiza su cantidad
            elementoParaAlmacenar.cantidad++;
            ActualizarUI();
            return;
        }

        ElementoEstanteria objetoVacio = ObtenerObjetoVacio();
        if (objetoVacio == null)
        {
            // Si no hay espacio en el inventario, no se puede añadir el elemento
            Debug.Log("No hay espacio en el inventario");
            return;
        }
        else
        {
            objetoVacio.cantidad = 1;
            objetoVacio.elemento = elemento;
            ActualizarUI();
            return;
        }
    }
    // Método para eliminar un elemento del inventario y soltarlo en la escena
    public void SoltarElemento(DatosElemento datosElemento)
    {



    }

    // Método para actualizar la interfaz de usuario del inventario
    void ActualizarUI()
    {
        for (int x = 0; x < elementosEstanteria.Length; x++)
        {
            if (elementosEstanteria[x].elemento != null)
                uiElementosEstanteria[x].Establecer(elementosEstanteria[x]);
            else
                uiElementosEstanteria[x].Limpiar();
        }

    }

    ElementoEstanteria ObtenerElementoAlmacenado(DatosElemento elemento)
    {
        // Devuelve el elemento almacenado en la posición indicada por el índice
        for (int i = 0; i < elementosEstanteria.Length; i++)
        {
            if (elementosEstanteria[i].elemento == elemento)
                return elementosEstanteria[i];
        }
        return null;
    }

    ElementoEstanteria ObtenerObjetoVacio()
    {
        for (int x = 0; x < elementosEstanteria.Length; x++)
        {
            if (elementosEstanteria[x].elemento == null)
                return elementosEstanteria[x];
        }
        return null;
    }

    public void ElementoSeleccionado(int indice)
    {
        if (elementosEstanteria[indice] == null)
            return;

        elementoSeleccionado = elementosEstanteria[indice];
        indiceElementoSeleccionado = indice;

        nombreElementoSeleccionado.text = elementoSeleccionado.elemento.nombre;
        descripcionElementoSeleccionado.text = elementoSeleccionado.elemento.descripcion;

        botonSoltar.SetActive(true);
        botonUsar.SetActive(true);
    }

    void EliminarElementoSeleccionado(int indice)
    {
        // Elimina el elemento seleccionado del inventario
        // y actualiza la interfaz de usuario

    }
    void LimpiarVentanaElementoSeleccionado()
    {
        elementoSeleccionado = null;
        nombreElementoSeleccionado.text = string.Empty;
        descripcionElementoSeleccionado.text = string.Empty;

        //se deshabilitan los botones
        botonUsar.SetActive(false);
        botonSoltar.SetActive(false);
    }

    public void OnBotonInventario(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            AbrirCerrarVentanaInventario();
    }
    public bool EstaAbierta()
    {
        //¿está activo en la jerarquía?
        return ventanaInventario.activeInHierarchy;
    }
    // public void OnBotonUsar() //eventos al pulsar el botón usar
    // {
    //     //se chequea el indicador que hay que modificar al usar
    //     switch(elementoSeleccionado.elemento.tipo)
    //     {
    //         case TipoUsoElemento.Salud:
    //             controladorIndicadores.Recuperarse(cantidadElemento);
    //             break;
    //         case TipoUsoElemento.Hambre:
    //             controladorIndicadores.Comer(cantidadElemento);
    //             break;
    //         case TipoUsoElemento.Sed:
    //             controladorIndicadores.Beber(cantidadElemento);
    //             break;
    //         case TipoUsoElemento.Descanso:
    //             controladorIndicadores.Descansar(cantidadElemento);
    //             break;
    //     }

    //     //tras usarlo, hay que eliminarlo del HUD de invemntario
    //     EliminarElementoSeleccionado();
    // }

    void EliminarElementoSeleccionado()
    {
        elementoSeleccionado.cantidad--;

        //si al decrementar es 0, hay que borrarlo del hueco
        if (elementoSeleccionado.cantidad == 0)
        {
            elementoSeleccionado.elemento = null;
            LimpiarVentanaElementoSeleccionado();
        }

        //siempre hay qeu actualizar la ventana tras añadir o eliminar
        ActualizarUI();
    }
    public void OnBotonSoltar(InputAction.CallbackContext context)
    {
        SoltarElemento(elementoSeleccionado.elemento);
        EliminarElementoSeleccionado();
    }

}
