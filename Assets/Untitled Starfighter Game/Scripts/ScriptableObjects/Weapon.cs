using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "USG/ScriptableObjects/Weapon", order = 0)]
public class Weapon : Equipment
{
    public Bullet bullet;

    public Weapon() : base() { type = EquipmentType.Weapon; }
}
