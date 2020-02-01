using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spaceship : MonoBehaviour
{
    [Header("Status Setting")]
    public float defaultDurability = 100;
    public float defaultArmour = 0;


    [Header("Parameters Setting")]
    public float defaultMaxMovementSpeed = 1;
    public float defaultAcrMovementSpeed = 1;
    public float defaultMaxRotationSpeed = 1;
    public float defaultAcrRotationSpeed = 1;

    public float Durability { get; private set; }
    public float Armour { get; private set; }
    public List<EquipmentObject> EquipmentObjects { get; private set; }

    [HideInInspector] public UnityEvent DestroyEvent;

    public virtual void InitializeStatus()
    {
        Durability = defaultDurability;
        Armour = defaultArmour;
        EquipmentObjects = new List<EquipmentObject>();
    }

    public virtual void ImpactDurability(int value)
    {
        Durability = Mathf.Clamp(Durability + value, 0, defaultDurability);

        if (Durability == 0) OnDestoryed();
    }

    public virtual void OnDestoryed()
    {
        DestroyEvent?.Invoke();
    }
}
