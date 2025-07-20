using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Structure/Repairs")]
public class Structureitem : ScriptableObject
{
    [Header("Config")]
    public string ID;
    public string Name;
    public GameObject prefabToRepair;
}
