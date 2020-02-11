using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser", menuName = "USG/ScriptableObjects/Laser", order = 1)]
public class Laser : Equipment
{
    public float range;
    public GameObject linePrefab;
    public GameObject impactPrefab;

    public Laser() : base(){ type = EquipmentType.Laser; }
}
