using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider),typeof(Rigidbody))]
public class Spaceship : MonoBehaviour
{
    [Header("Preset objects")]
    public LayerMask asteroidLayer;
    public Transform laserStartPoint;
    public Transform shootingStartPoints;
    public Equipment defaultEquipment;

    [Header("Status Setting")]
    public float defaultDurability = 100;
    public float defaultArmour = 0;

    [Header("Parameters Setting")]
    public float defaultMaxMovementSpeed = 1;
    public float defaultAcrMovementSpeed = 1;
    public float defaultMaxRotationSpeed = 10;
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
    private Vector3 laserEndPoint;
    private LineRenderer laserLineRenderer;
    private ParticleSystem laserImpactEffect;

    public delegate void ValueChangeDelegate(float curV, float maxV);
    public ValueChangeDelegate OnDurabilityChangedEvent;
    public ValueChangeDelegate OnVolumeChangedEvent;
    [HideInInspector] public UnityEvent OnDeathEvent;
    [HideInInspector] public UnityEvent OnSwitchEquipmentEvent;

    protected virtual void Update()
    {
        if (!SelectedEquipmentObject.Triggerable) SelectedEquipmentObject.UpdateTimer(-Time.deltaTime);
        reloadTimer -= Time.deltaTime;
        if (reloadTimer < 0) {
            ResetEquipmentVolume();
        }

        if(IsDeath&& Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
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

    public virtual void SwitchEquipment(int id)
    {
        if(SelectedEquipmentObject.Template.type!= EquipmentObjects[id].Template.type) {
            OnSwitchEquipmentEvent?.Invoke();
        }
        SelectEquipmentID = id;
        switch (SelectedEquipmentObject.Template.type) {
            case EquipmentType.Weapon:
                SwitchToWeaponTypeEquipment(SelectedEquipmentObject.Template as Weapon);
                break;
            case EquipmentType.Laser:
                SwitchToLaserTypeEquipment(SelectedEquipmentObject.Template as Laser);
                break;
            default:
                break;
        }
        
    }

    public virtual void Shoot()
    {
        if (SelectedEquipmentObject.Volume == 0) Reload();
        if (!SelectedEquipmentObject.Triggerable) return;
        reloadTimer = SelectedEquipmentObject.Template.reloadDuration;

        Equipment equipment = SelectedEquipmentObject.Template;
        SelectedEquipmentObject.ResetTimer();      
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
        if (SelectedEquipmentObject.Reloading || SelectedEquipmentObject.VolumeInfinite) return;
        SelectedEquipmentObject.SetReload(true);
        UIManager.Instance.SetReload();
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
        OnDeathEvent?.Invoke();
    }

    protected virtual void SwitchToWeaponTypeEquipment(Weapon weapon)
    {
        OnVolumeChangedEvent?.Invoke(SelectedEquipmentObject.Volume, SelectedEquipmentObject.Template.volume);
    }

    protected virtual void SwitchToLaserTypeEquipment(Laser laser)
    {
        if (laserLineRenderer) Destroy(laserLineRenderer.gameObject);
        if (laserImpactEffect) Destroy(laserImpactEffect.gameObject);
        laserLineRenderer = Instantiate(laser.linePrefab).GetComponent<LineRenderer>();
        laserLineRenderer.SetPosition(0, laserStartPoint.position);
        laserLineRenderer.SetPosition(1, laserStartPoint.position);
        laserImpactEffect = Instantiate(laser.impactPrefab).GetComponent<ParticleSystem>();
        OnVolumeChangedEvent?.Invoke(0, 0);
    }

    protected virtual void ShootWithWeapon(Weapon weapon)
    {
        ImpactEquipmentVolume(-1);
        for (int i = 0; i < shootingStartPoints.childCount; i++) {
            Instantiate(weapon.bullet.prefab, shootingStartPoints.GetChild(i).position, Quaternion.identity, bulletsHolder).
                GetComponent<BulletController>().InitializeBullet(gameObject.layer, weapon.bullet, transform.forward);
        }
    }

    protected virtual GameObject ShootWithLaser(Laser laser)
    {
        var colliders = Physics.OverlapSphere(laserStartPoint.position, laser.range, asteroidLayer);
        if (colliders.Length != 0) {
            {
                //int minID = -1;
                //float minDistance = Vector3.Distance(colliders[0].transform.position, laserStartPoint.position);
                //for (int i = 0; i < colliders.Length; i++) {
                //    if (colliders[i].transform.position.y > laserStartPoint.position.y) continue;
                //    float distance = Vector3.Distance(colliders[i].transform.position, laserStartPoint.position);
                //    if (distance <= minDistance) {
                //        minID = i;
                //        minDistance = distance;
                //    }
                //}
                //if (minID == -1) return;
            }
            if (Physics.Raycast(laserStartPoint.position, colliders[0].transform.position - laserStartPoint.position,
                out RaycastHit hitInfo, laser.range, asteroidLayer)) {
                laserLineRenderer.SetPosition(0, laserStartPoint.position);
                laserLineRenderer.SetPosition(1, hitInfo.point);
                if (laserImpactEffect.isStopped) laserImpactEffect.Play();
                laserImpactEffect.transform.position = hitInfo.point;
                return hitInfo.collider.gameObject;
            }
            else {
                laserLineRenderer.SetPosition(0, laserStartPoint.position);
                laserLineRenderer.SetPosition(1, laserStartPoint.position);
                laserImpactEffect.Stop();
            }
        }
        else {
            laserLineRenderer.SetPosition(0, laserStartPoint.position);
            laserLineRenderer.SetPosition(1, laserStartPoint.position);
            laserImpactEffect.Stop();
        }
        return null;
    }
}
