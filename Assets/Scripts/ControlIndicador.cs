using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ControlIndicador : MonoBehaviour, IDeterioro
{

    public Indicador vida;
    public Indicador hambre;
    public Indicador sed;
    public Indicador energia;
    public UnityEvent OnSufrirDeterioro; // Evento para notificar el deterioro

    //Variables para quitar vida cuando las barras de hambre, sed y energia bajen a 0
    public float tiempoVida = 0.0f; // Tiempo que pasa antes de quitar vida
    public float reducirVidaHambre = 0.0f; // Cantidad de vida que se quita por hambre
    public float reducirVidaSed = 0.0f; // Cantidad de vida que se quita por sed

    // Start is called before the first frame update
    void Start()
    {
        hambre.valorActual = hambre.valorInicial;
        sed.valorActual = sed.valorInicial;
        energia.valorActual = energia.valorInicial;
        vida.valorActual = vida.valorInicial;
    }

    // Update is called once per frame
    void Update()
    {
        energia.Restar(energia.indiceDeterioro * Time.deltaTime);
        
        if (hambre.valorActual == 0.0f)
            vida.Restar(reducirVidaHambre * Time.deltaTime);
            else
            hambre.Restar(hambre.indiceDeterioro * Time.deltaTime);


        if (sed.valorActual == 0.0f)
            vida.Restar(reducirVidaSed * Time.deltaTime);
            else
            sed.Restar(sed.indiceDeterioro * Time.deltaTime);

        vida.barra.fillAmount = vida.ObtenerPorcentaje();
        hambre.barra.fillAmount = hambre.ObtenerPorcentaje();
        sed.barra.fillAmount = sed.ObtenerPorcentaje();
        energia.barra.fillAmount = energia.ObtenerPorcentaje();
        
        Muerto();
    }

    //metodos asociados
    public void Muerto()
    {
        if (vida.valorActual == 0.0f)
        {
            // Aquí puedes agregar la lógica para lo que sucede cuando el indicador llega a 0
            Debug.Log("El indicador ha llegado a 0.");
        }
    }
    //Estos cuatro son sustituibles por un  switch dependiendo de la tecla que se pulse
    public void Recuperar(float cantidad)
    {
        vida.Sumar(cantidad);
    }
    public void Comer(float cantidad)
    {
        hambre.Sumar(cantidad);
    }
    public void Beber(float cantidad)
    {
        sed.Sumar(cantidad);
    }
    public void Dormir(float cantidad)
    {
        energia.Sumar(cantidad);
    }
 
    public void IncrementarValores(float cantidad, string codigo)
    {
        switch (codigo.ToLower())
        {
            case "vida":
                vida.Sumar(cantidad);
                break;
            case "hambre":
                hambre.Sumar(cantidad);
                break;
            case "sed":
                sed.Sumar(cantidad);
                break;
            case "energia":
                energia.Sumar(cantidad);
                break;
            default:
                Debug.LogWarning("Código no existente: " + codigo);
                break;
        }
    }

    //Metodo para producir deterioro
    public void ProducirDeterioro(float cantidad)
    {
        vida.Restar(cantidad);
        OnSufrirDeterioro?.Invoke();

    }
    


}
[System.Serializable]
public class Indicador
{
    [Header("Variables")]
    public float valorMaximo; // Valor máximo del indicador
    public float valorInicial; // Valor inicial del indicador
    //[HideInInspector]
    public float valorActual; // Valor actual del indicador
    public float indiceCambio; // Indice de cambio del indicador
    public float indiceDeterioro; // Indice de deterioro del indicador

    public Image barra; // Barra del indicador
   
    public void Sumar (float cantidad)
    {
        // valorActual += cantidad * indiceCambio;
        // if (valorActual > valorMaximo)
        //     valorActual = valorMaximo;
        
        valorActual = Mathf.Min(valorActual + cantidad * indiceCambio, valorMaximo);
    }

    public void Restar (float cantidad)
    {
        // valorActual -= cantidad * indiceDeterioro;
        // if (valorActual < 0.0f)
        //     valorActual = 0.0f;

        valorActual = Mathf.Max(valorActual - cantidad * indiceDeterioro, 0.0f);
    }

    public float ObtenerPorcentaje ()
    {
        return valorActual / valorMaximo;
        //barra.fillAmount = porcentaje;
    }
}

public interface IDeterioro
{
    void ProducirDeterioro(float cantidad);
    

}
