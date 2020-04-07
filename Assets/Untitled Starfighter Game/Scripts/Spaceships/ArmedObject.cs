using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedObject : EntityObject
{
    public static Transform BulletsHolder { get; protected set; }

    [Header("Arm Parameters")]
    public Equipment defaultMainEquipment;
    public Transform shootStartPoints;

    protected EquipmentObject MainEquipment { get {
            if (equipmentObjects.Count > selectEquipmentID) {
                return equipmentObjects[selectEquipmentID];
            }
            else {
                Debug.LogError("Invalid selection ID"); return null;
            }
        } }
    protected int selectEquipmentID;
    protected List<EquipmentObject> equipmentObjects;

    // Auto reload volume after period without shooting
    protected float autoReloadTimer;

    public ValueChangeDelegate OnVolumeChangedEvent;

    protected override void Awake()
    {
        if (!BulletsHolder) BulletsHolder = new GameObject("bullets holder").transform;

        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        if (!MainEquipment.Triggerable) MainEquipment.UpdateTimer(-Time.deltaTime);
        autoReloadTimer -= Time.deltaTime;
        if (autoReloadTimer < 0) {
            ResetEquipmentVolume();
        }
    }

    protected override void InitializeDefaultParameters()
    {
        base.InitializeDefaultParameters();

        equipmentObjects = new List<EquipmentObject> {
            new EquipmentObject(defaultMainEquipment.Hash)
        };
    }

    protected virtual void ImpactEquipmentVolume(float value)
    {
        MainEquipment.UpdateVolume(value);
        OnVolumeChangedEvent?.Invoke(MainEquipment.Volume, MainEquipment.Template.volume);
    }

    protected virtual void ResetEquipmentVolume()
    {
        autoReloadTimer = MainEquipment.Template.reloadDuration;
        MainEquipment.ResetVolume();
        OnVolumeChangedEvent?.Invoke(MainEquipment.Volume, MainEquipment.Template.volume);
    }

    protected virtual bool TryShoot()
    {
        //Debug.Log("Try shoot");
        if (!MainEquipment.Triggerable) {
            if (MainEquipment.Volume == 0) Reload();
            return false;
        }
        autoReloadTimer = MainEquipment.Template.reloadDuration;

        MainEquipment.ResetTimer();
        ShootWithWeapon();
        return true;
    }

    public virtual void Reload()
    {
        //Debug.Log("Reload");
        if (!MainEquipment.Reloading && !MainEquipment.VolumeInfinite && MainEquipment.Volume != MainEquipment.Template.volume) {
            MainEquipment.SetReload(true);
            StartCoroutine(WaitForReload());
        }
    }

    protected virtual IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(MainEquipment.Template.reloadDuration);
        ResetEquipmentVolume();
        MainEquipment.SetReload(false);
    }

    protected virtual void ShootWithWeapon()
    {
        //Debug.Log("Shoot with weapon");
        Weapon weapon = MainEquipment.Template as Weapon;
        ImpactEquipmentVolume(-1);
        for (int i = 0; i < shootStartPoints.childCount; i++) {
            Instantiate(weapon.bullet.prefab, shootStartPoints.GetChild(i).position, Quaternion.identity, BulletsHolder).
                GetComponent<BulletController>().InitializeBullet(gameObject.layer, weapon.bullet, shootStartPoints.forward);
        }
    }
}
