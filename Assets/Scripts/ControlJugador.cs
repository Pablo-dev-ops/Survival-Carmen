using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ControlJugador : MonoBehaviour
{

    private Vector2 ratonDelta;
    [Header("Vista Camara")]
    public Transform camara;
    public float minVistaX, maxVistaX;
    public float sensibilidadRaton;

    private float rotacionActualCamara;

    [Header("Movimiento ")]
    public float velocidadMovimiento;
    private Vector2 movimientoActualEntrada;
    private Rigidbody rb;

    [Header("Salto")]
    public float fuerzaSalto;
    public LayerMask capaSuelo;


    bool puedeMirar = true; //Variable para controlar si el jugador puede mirar o no
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; //Bloqueamos el cursor en el centro de la pantalla
        ModoInventario(false); //Desbloqueamos el cursor al iniciar el juego

    }
    void FixedUpdate()
    {
        Movimiento();

    }

    void Update()
    {
        //Debug.Log("Estoy funcionando, no me pegues!");
    }

    void LateUpdate()
    {
        VistaCamara();
    }

    //Capturamos el input del raton
    public void OnVistaInput(InputAction.CallbackContext context)
    {
        ratonDelta = context.ReadValue<Vector2>();
    }

    //Giramos la cámara y limitamos la rotación en el eje X
    private void VistaCamara()
    {
        if (puedeMirar)
        {
            rotacionActualCamara += ratonDelta.y * sensibilidadRaton;
            rotacionActualCamara = Mathf.Clamp(rotacionActualCamara, minVistaX, maxVistaX);
            //Rotacion horaria positiva, por lo que tenemos que multiplicar por -1
            camara.localEulerAngles = new Vector3(-rotacionActualCamara, 0, 0);
            transform.eulerAngles += new Vector3(0, ratonDelta.x * sensibilidadRaton, 0);
        }
    }
    //Accion de MOVIMIENTO
    private void Movimiento()
    {
        Vector3 direccion = transform.forward * movimientoActualEntrada.y
                          + transform.right * movimientoActualEntrada.x;
        direccion *= velocidadMovimiento;
        direccion.y = rb.velocity.y; //Fijamos la velocidad en el eje Y
        rb.velocity = direccion; //Asignamos la velocidad al rigidbody
    }

    //Capturamos el input del teclado
    public void OnMovimientoInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            movimientoActualEntrada = context.ReadValue<Vector2>();
        else if (context.phase == InputActionPhase.Canceled)
            movimientoActualEntrada = Vector2.zero;
    }

    //Accion de SALTO
    public void OnSaltoInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (EstaEnSUelo()) //Comprobamos si el jugador está en el suelo
                rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    private bool EstaEnSUelo()
    {
        if (rb == null) return false;

        Ray[] ray = new Ray[4]; //Creamos un array de rayos para comprobar si el jugador está en el suelo
        {
            ray[0] = new Ray(transform.position + transform.forward * 0.2f, Vector3.down);
            ray[1] = new Ray(transform.position + (-transform.forward * 0.2f), Vector3.down);
            ray[2] = new Ray(transform.position + transform.right * 0.2f, Vector3.down);
            ray[3] = new Ray(transform.position + (-transform.right * 0.2f), Vector3.down);
        }

        for (int i = 0; i < ray.Length; i++)
        {
            if (Physics.Raycast(ray[i], 1f, capaSuelo)) //Si el rayo colisiona con la capa de suelo, devolvemos true
                return true;

        }
        return false; //Si no ha colisionado con nada, devolvemos false
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + transform.forward * 0.2f, Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + transform.right * 0.2f, Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    public void ModoInventario(bool valor)
    {
        Cursor.lockState = valor ? CursorLockMode.None : CursorLockMode.Locked; //Desbloqueamos el cursor si el jugador está en modo inventario
        puedeMirar = !valor; //Si el jugador está en modo inventario, no puede mirar
    }
}
