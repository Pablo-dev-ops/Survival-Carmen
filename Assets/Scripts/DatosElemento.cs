using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu (fileName = "Elemento", menuName = "Elemento inventario")]
public class DatosElemento : ScriptableObject
{
    [Header ("Informacion")]
    public string nombre; // Nombre del elemento
    public string descripcion; // Descripción del elemento
    public Sprite icono; // Icono del elemento
    public GameObject prefab; // Prefab del elemento


}
