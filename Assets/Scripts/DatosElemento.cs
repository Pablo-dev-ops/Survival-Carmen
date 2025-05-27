using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

//se define un tipo de dato compuesto
public enum TipoUsoElemento
{
    Comida,
    Bebida,
    Salud,
    Descanso
}
[CreateAssetMenu(fileName = "Elemento", menuName = "Elemento inventario")]

public class DatosElemento : ScriptableObject
{
    [Header("Informacion")]
    public string nombre; // Nombre del elemento
    public string descripcion; // Descripción del elemento
    public Sprite icono; // Icono del elemento
    public GameObject prefab; // Prefab del elemento

    public TipoUsoElemento tipo; // Tipo de elemento (Comida, Bebida, etc.)
}
