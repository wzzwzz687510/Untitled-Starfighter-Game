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

    public float Durability { get; protected set; }
    public float Armour { get; protected set; }
    public bool IsDeath { get; protected set; }
    public List<EquipmentObject> EquipmentObjects { get; protected set; }



    public delegate void DurabilityChangeDelegate(float curD, float maxD);
    public DurabilityChangeDelegate OnDurabilityChangedEvent;
    [HideInInspector] public UnityEvent DestroyEvent;

    public virtual void InitializeStatus()
    {
        Durability = defaultDurability;
        Armour = defaultArmour;
        EquipmentObjects = new List<EquipmentObject>();
    }

    public virtual void ImpactDurability(float value)
    {
        if (IsDeath) return;
        Durability = Mathf.Clamp(Durability + value, 0, defaultDurability);
        OnDurabilityChangedEvent?.Invoke(Durability, defaultDurability);
        if (Durability == 0) OnDestoryed();
    }

    protected virtual void OnDestoryed()
    {
        IsDeath = true;
        DestroyEvent?.Invoke();
    }
}
