using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody),typeof(SpaceShipController))]
public class PlayerSpaceship : Spaceship
{
    public static PlayerSpaceship MainCharacter { get; protected set; }

    [Header("Addon Setting")]
    public Laser defaultLaser;
    public float resources;
    public float dodgeTime = 0.6f;

    public bool IsOutsideBoundary { get; protected set; }
    public SpaceShipController Controller { get; protected set; }

    protected Spaceship shootTarget;
    protected bool hasTarget;

    public ValueChangeDelegate OnResourceChangedEvent;
    [HideInInspector] public UnityEvent OnBoundaryEvent;

    private Rigidbody m_rb;

    private void Awake()
    {
        if (!MainCharacter) MainCharacter = this;
        Controller = GetComponent<SpaceShipController>();
        m_rb = GetComponent<Rigidbody>();

        InitializeStatus();
        EquipmentObjects.Add(new EquipmentObject(defaultLaser.Hash));
        resources = 0;
    }

    private void FixedUpdate()
    {
        if (IsDeath) return;
        CheckIsInsideBoundary();
    }

    private void CheckIsInsideBoundary()
    {
        bool rt = LevelManager.Instance.CheckIsInsideBoundary(transform.position);
        if (!rt) {
            Vector3 toBoundary = LevelManager.Instance.GetToBoundaryVector(transform.position);
            m_rb.AddForce(100*toBoundary);
            ImpactDurability(-toBoundary.magnitude * Time.deltaTime);
            Debug.Log("Out Boundary");
            if (!IsOutsideBoundary) {
                IsOutsideBoundary = true;
                OnBoundaryEvent?.Invoke();
            }
        }
        else if (IsOutsideBoundary) {
            IsOutsideBoundary = false;
            OnBoundaryEvent?.Invoke();
        }
    }

    protected override void OnDestoryed()
    {
        Controller.enabled = false;
        base.OnDestoryed();
    }

    protected override GameObject ShootWithLaser(Laser laser)
    {
        GameObject asteroidGO = base.ShootWithLaser(laser);
        if (asteroidGO != null) {
            asteroidGO.GetComponent<Asteroid>().ImpactDurability(-100*Time.deltaTime);
            resources += 100*Time.deltaTime;
            OnResourceChangedEvent?.Invoke((int)resources,0);
        }
        return asteroidGO;
    }

    protected override void ShootWithWeapon(Weapon weapon)
    {
        ImpactEquipmentVolume(-1);
        for (int i = 0; i < shootingStartPoints.childCount; i++) {
            Instantiate(weapon.bullet.prefab, shootingStartPoints.GetChild(i).position, Quaternion.identity, bulletsHolder).
                GetComponent<BulletController>().InitializeBullet(gameObject.layer, weapon.bullet,
                hasTarget && shootTarget != null ? shootTarget.transform.position - shootingStartPoints.GetChild(i).position : transform.forward);
        }
    }

    public void ImpactResources(int num)
    {
        resources = Mathf.Max(0, resources + num);
        OnResourceChangedEvent?.Invoke((int)resources, 0);
    }

    public void SetShootTargetPosition(bool hasTarget,Spaceship target)
    {
        this.hasTarget = hasTarget;
        if (hasTarget && shootTarget != target) {
            if(shootTarget!=null) shootTarget.SetHighlight(false);
            shootTarget = target;
            shootTarget.SetHighlight(true);
        }       
    }

    public void SetInvincible(bool bl)
    {
        Invincible = bl;
    }
}
