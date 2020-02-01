using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentObject : MonoBehaviour
{
    public int TemplateHash { get; private set; }

    public float Volume { get; private set; }
    public float Durability { get; private set; }

    public void InitializeEquipment(int hash)
    {
        TemplateHash = hash;

        Equipment eq = hash.GetEquipment();
        Volume = eq.volume;
        Durability = eq.durability;
    }
}
