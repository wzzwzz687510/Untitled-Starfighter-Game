using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpaceship : Spaceship
{
    public static PlayerSpaceship MainCharacter { get; protected set; }

    [Header("Player Ship Parameters")]
    public Transform laserStartPoint;
    protected Vector3 laserEndPoint;
    protected LineRenderer laserLineRenderer;
    protected ParticleSystem laserImpactEffect;

    public LayerMask asteroidLayer;
    public Laser defaultLaser;
    public float laserLimitHeat=10;
    public float resources;
    public float dodgeTime = 0.6f;

    public float TopArmour { get; protected set; }
    public float BottomArmour { get; protected set; }
    public float LeftArmour { get; protected set; }
    public float RightArmour { get; protected set; }

    public float LaserHeat { get; protected set; }
    public bool LaserStun { get; protected set; }
    public bool IsOutsideBoundary { get; protected set; }
    public SpaceShipController Controller { get; protected set; }

    protected EntityObject shootTarget;
    protected bool hasTarget;

    public ValueChangeDelegate OnResourceChangedEvent;
    [HideInInspector] public UnityEvent OnSpaceShipOutBoundaryEvent;
    [HideInInspector] public UnityEvent OnEquipmentSwitchedEvent;
    [HideInInspector] public UnityEvent OnLaserHeatChangedEvent;

    private Rigidbody m_rb;

    #region Overrided Function
    public override void Reload()
    {
        base.Reload();

        UIManager.Instance.SetReload();
    }

    public override void OnShooted(Vector3 direction, float damage)
    {
        float dotValue = Vector3.Dot(transform.up, direction);
        if (dotValue > 0.5) {
            // Deal up damage;
            damage -= TopArmour;
            if (damage > 0) {
                TopArmour = 0;
            }
            else {
                TopArmour = -damage;
                damage = 0;
            }
        }
        else if (dotValue < -0.5) {
            // Deal down damage;
            damage -= BottomArmour;
            if (damage > 0) {
                BottomArmour = 0;
            }
            else {
                BottomArmour = -damage;
                damage = 0;
            }
        }
        else {
            if (Vector3.Dot(transform.right, direction) > 0) {
                // Deal right damage;
                damage -= RightArmour;
                if (damage > 0) {
                    RightArmour = 0;
                }
                else {
                    RightArmour = -damage;
                    damage = 0;
                }
            }
            else {
                // Deal left damage;
                damage -= LeftArmour;
                if (damage > 0) {
                    LeftArmour = 0;
                }
                else {
                    LeftArmour = -damage;
                    damage = 0;
                }
            }
        }
        OnAmourChangeEvent?.Invoke(0, 0);

        ImpactDurability(-damage);
    }

    protected override void Awake()
    {
        if (!MainCharacter) MainCharacter = this;
        Controller = GetComponent<SpaceShipController>();
        m_rb = GetComponent<Rigidbody>();

        base.Awake();       
    }

    protected override void InitializeDefaultParameters()
    {
        base.InitializeDefaultParameters();
        equipmentObjects.Add(new EquipmentObject(defaultLaser.Hash));

        LeftArmour = defaultArmour;
        RightArmour = defaultArmour;
        TopArmour = defaultArmour;
        BottomArmour = defaultArmour;

        resources = 200;
    }

    protected override void InitializeAfterAwakeParameters()
    {
        base.InitializeAfterAwakeParameters();

        //OnResourceChangedEvent?.Invoke((int)resources, 0);
        //OnAmourChangeEvent?.Invoke(0, 0);
    }

    protected override bool TryShoot()
    {
        if (!MainEquipment.Triggerable) {
            if (MainEquipment.Volume == 0) Reload();
            return false;
        }
        autoReloadTimer = MainEquipment.Template.reloadDuration;

        MainEquipment.ResetTimer();
        switch (MainEquipment.Template.type) {
            case EquipmentType.Weapon:
                ShootWithWeapon(MainEquipment.Template as Weapon);
                break;
            case EquipmentType.Laser:
                ShootWithLaser(MainEquipment.Template as Laser);
                break;
            default:
                break;
        }

        return true;
    }

    protected override void OnDestoryed()
    {
        Controller.enabled = false;
        OnDestoryedEvent?.Invoke();
    }
    #endregion
    public void UpgradeArmour()
    {
        MaxArmour += 2;
        TopArmour = MaxArmour;
        BottomArmour = MaxArmour;
        LeftArmour = MaxArmour;
        RightArmour = MaxArmour;
        OnAmourChangeEvent?.Invoke(0, 0);
    }

    public void ImpactResources(float num)
    {
        resources = Mathf.Max(0, resources + num);
        OnResourceChangedEvent?.Invoke((int)resources, 0);
    }

    public void SetShootTargetPosition(bool hasTarget, EntityObject target)
    {
        this.hasTarget = hasTarget;
        if (hasTarget && shootTarget != target) {
            if (shootTarget != null) shootTarget.SetHighlight(false);
            shootTarget = target;
            shootTarget.SetHighlight(true);
        }
    }

    public void SetInvincible(bool bl)
    {
        Invincible = bl;
    }

    public void StopLaser()
    {
        if (selectEquipmentID == 1) {
            laserLineRenderer.SetPosition(0, laserStartPoint.position);
            laserLineRenderer.SetPosition(1, laserStartPoint.position);
            laserImpactEffect.Stop();
        }
    }

    public virtual void SwitchEquipment(int id)
    {
        StopLaser();

        if (MainEquipment.Template.type != equipmentObjects[id].Template.type) {
            OnEquipmentSwitchedEvent?.Invoke();
        }
        selectEquipmentID = id;
        switch (MainEquipment.Template.type) {
            case EquipmentType.Weapon:
                SwitchToWeaponTypeEquipment(MainEquipment.Template as Weapon);
                break;
            case EquipmentType.Laser:
                SwitchToLaserTypeEquipment(MainEquipment.Template as Laser);
                break;
            default:
                break;
        }
    }

    public void OnFireInput()
    {
        TryShoot();
    }

    protected virtual void SwitchToWeaponTypeEquipment(Weapon weapon)
    {
        OnVolumeChangedEvent?.Invoke(MainEquipment.Volume, MainEquipment.Template.volume);
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

    protected virtual void ShootWithLaser(Laser laser)
    {
        if (LaserStun) return;
        LaserHeat += 20 * Time.deltaTime;
        LaserStun = LaserHeat > laserLimitHeat;

        GameObject asteroidGO = GetLaserTarget(laser);
        if (asteroidGO != null) {            
            resources += asteroidGO.GetComponent<Asteroid>().ImpactDurability(-300 * Time.deltaTime);
            OnResourceChangedEvent?.Invoke((int)resources,0);
        }
    }

    protected virtual void ShootWithWeapon(Weapon weapon)
    {
        ImpactEquipmentVolume(-1);
        for (int i = 0; i < shootStartPoints.childCount; i++) {
            Instantiate(weapon.bullet.prefab, shootStartPoints.GetChild(i).position, Quaternion.identity, BulletsHolder).
                GetComponent<BulletController>().InitializeBullet(gameObject.layer, weapon.bullet,
                hasTarget && shootTarget != null ? shootTarget.transform.position - shootStartPoints.GetChild(i).position : transform.forward);
        }
    }

    protected GameObject GetLaserTarget(Laser laser)
    {
        var colliders = Physics.OverlapSphere(laserStartPoint.position, laser.range, asteroidLayer);
        
        if (colliders.Length != 0) {
            Vector3 targetPosition = colliders[0].transform.position;
            float minAngle = Vector3.Angle(transform.forward, targetPosition - transform.position);
            foreach (var target in colliders) {
                float newAngle = Vector3.Angle(transform.forward, target.transform.position - transform.position);
                if (minAngle > newAngle) {
                    minAngle = newAngle;
                    targetPosition = target.transform.position;
                }
            }

            if (Physics.Raycast(laserStartPoint.position, targetPosition - laserStartPoint.position,
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

    private void FixedUpdate()
    {
        if (IsDestroyed) return;
        CheckIsInsideBoundary();

        LaserHeat = Mathf.Max(0, LaserHeat - Time.deltaTime);
        if (LaserStun) {
            StopLaser();
            LaserStun = LaserHeat > 0;
        }
        OnLaserHeatChangedEvent?.Invoke();
    }

    private void CheckIsInsideBoundary()
    {
        bool rt = LevelManager.Instance.CheckIsInsideBoundary(transform.position);
        if (!rt) {
            Vector3 toBoundary = LevelManager.Instance.GetToBoundaryVector(transform.position);
            m_rb.AddForce(100 * toBoundary);
            Debug.Log("Out Boundary");
            if (!IsOutsideBoundary) {
                IsOutsideBoundary = true;
                OnSpaceShipOutBoundaryEvent?.Invoke();
            }
        }
        else if (IsOutsideBoundary) {
            IsOutsideBoundary = false;
            OnSpaceShipOutBoundaryEvent?.Invoke();
        }
    }
}
