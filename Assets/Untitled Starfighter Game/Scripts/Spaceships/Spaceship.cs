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
    public float Durability { get; protected set; }
    public float Armour { get; protected set; }
    public bool IsDeath { get; protected set; }

    public int SelectEquipmentID { get; protected set; }
    public List<EquipmentObject> EquipmentObjects { get; protected set; }
    public EquipmentObject SelectedEquipmentObject => EquipmentObjects[SelectEquipmentID];

    private Transform bulletsHolder;
    private float reloadTimer;

    public delegate void ValueChangeDelegate(float curV, float maxV);
    public ValueChangeDelegate OnDurabilityChangedEvent;
    public ValueChangeDelegate OnVolumeChangedEvent;
    [HideInInspector] public UnityEvent DestroyEvent;

    protected virtual void Update()
    {
        if (!SelectedEquipmentObject.Triggerable) SelectedEquipmentObject.UpdateTimer(-Time.deltaTime);
        reloadTimer -= Time.deltaTime;
        if (reloadTimer < 0) {
            ResetEquipmentVolume();
        }
    }

    public virtual void InitializeStatus()
    {
        Durability = defaultDurability;
        Armour = defaultArmour;
        EquipmentObjects = new List<EquipmentObject> {
            new EquipmentObject(defaultEquipment.Hash)
        };
        bulletsHolder = new GameObject("bullets holder").transform;
        State = new TransformState();
        State.SetFromTransform(transform);
        SelectEquipmentID = 0;
    }

    public virtual void ImpactDurability(float value)
    {
        if (IsDeath) return;
        Durability = Mathf.Clamp(Durability + value, 0, defaultDurability);
        OnDurabilityChangedEvent?.Invoke(Durability, defaultDurability);
        if (Durability == 0) OnDestoryed();
    }

    public virtual void ImpactEquipmentVolume(float value)
    {
        SelectedEquipmentObject.UpdateVolume(value);
        OnVolumeChangedEvent?.Invoke(SelectedEquipmentObject.Volume, SelectedEquipmentObject.Template.volume);
    }

    public virtual void ResetEquipmentVolume()
    {
        reloadTimer = SelectedEquipmentObject.Template.reloadDuration;
        SelectedEquipmentObject.ResetVolume();
        OnVolumeChangedEvent?.Invoke(SelectedEquipmentObject.Volume, SelectedEquipmentObject.Template.volume);
    }

    public virtual void Shoot()
    {
        if (!SelectedEquipmentObject.Triggerable) return;
        reloadTimer = SelectedEquipmentObject.Template.reloadDuration;

        Equipment equipment = SelectedEquipmentObject.Template;
        SelectedEquipmentObject.ResetTimer();
        ImpactEquipmentVolume(-1);
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

    public virtual void Reload()
    {
        if (SelectedEquipmentObject.Reloading) return;

        SelectedEquipmentObject.SetReload(true);
        StartCoroutine(WaitForReload());
    }

    protected virtual IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(SelectedEquipmentObject.Template.reloadDuration);
        ResetEquipmentVolume();
        SelectedEquipmentObject.SetReload(false);
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
            Instantiate(bulletPrefab.gameObject, shootingStartPoint.GetChild(i).position, Quaternion.identity, bulletsHolder).
                GetComponent<BulletController>().InitializeBullet(gameObject.layer, weapon.bullet, -transform.forward.normalized);
        }
    }

    protected virtual void ShootWithLaser(Laser laser)
    {

    }
}
