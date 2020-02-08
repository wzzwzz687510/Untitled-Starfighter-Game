using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spaceship : MonoBehaviour
{
    [Header("Preset objects")]
    public Transform shootingStartPoint;
    public BulletController bulletPrefab;
    public Equipment defaultEquipment;

    [Header("Status Setting")]
    public float defaultDurability = 100;
    public float defaultArmour = 0;


    [Header("Parameters Setting")]
    public float defaultMaxMovementSpeed = 1;
    public float defaultAcrMovementSpeed = 1;
    public float defaultMaxRotationSpeed = 1;
    public float defaultAcrRotationSpeed = 1;

    public TransformState State { get; protected set; }
    public int SelectEquipmentHash { get; protected set; }
    public float EquipmentTimer { get; protected set; }
    public float Durability { get; protected set; }
    public float Armour { get; protected set; }
    public bool IsDeath { get; protected set; }
    public List<EquipmentObject> EquipmentObjects { get; protected set; }

    private Transform bulletsHolder;

    public delegate void DurabilityChangeDelegate(float curD, float maxD);
    public DurabilityChangeDelegate OnDurabilityChangedEvent;
    [HideInInspector] public UnityEvent DestroyEvent;

    protected virtual void Update()
    {
        if (EquipmentTimer > 0) EquipmentTimer -= Time.deltaTime;
    }

    public virtual void InitializeStatus()
    {
        Durability = defaultDurability;
        Armour = defaultArmour;
        EquipmentObjects = new List<EquipmentObject>();
        bulletsHolder = new GameObject("bullets holder").transform;
        //bulletsHolder.SetParent(transform);
        State = new TransformState();
        State.SetFromTransform(transform);
        SelectEquipmentHash = defaultEquipment.Hash;
    }

    public virtual void ImpactDurability(float value)
    {
        if (IsDeath) return;
        Durability = Mathf.Clamp(Durability + value, 0, defaultDurability);
        OnDurabilityChangedEvent?.Invoke(Durability, defaultDurability);
        if (Durability == 0) OnDestoryed();
    }

    public virtual void Shoot()
    {
        if (EquipmentTimer > 0) return;        
        Equipment equipment = SelectEquipmentHash.GetEquipment();
        EquipmentTimer = equipment.triggerInterval;
        switch (equipment.type) {
            case EquipmentType.Weapon:
                ShootWithWeapon(equipment as Weapon);
                break;
            case EquipmentType.Laser:
                ShootWithLaser(equipment as Laser);
                break;
            default:
                break;
        }
    }

    public virtual void UpdateTransformState(TransformState target, float positionLerpPct, float rotationLerpPct)
    {
        State.LerpTowards(target, positionLerpPct, rotationLerpPct);

        State.UpdateTransform(transform);
    }

    protected virtual void OnDestoryed()
    {
        IsDeath = true;
        DestroyEvent?.Invoke();
    }

    protected virtual void ShootWithWeapon(Weapon weapon)
    {
        for (int i = 0; i < shootingStartPoint.childCount; i++) {
            Instantiate(bulletPrefab.gameObject, shootingStartPoint.GetChild(i).position,Quaternion.identity, bulletsHolder).
                GetComponent<BulletController>().InitializeBullet(weapon.bullet, -transform.forward.normalized); 
        }
    }

    protected virtual void ShootWithLaser(Laser laser)
    {

    }
}
