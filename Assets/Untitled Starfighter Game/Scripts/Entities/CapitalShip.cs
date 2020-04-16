using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CapitalShip : Spaceship
{
    [Header("Capital Parameters")]
    public float magnification = 10;
    public float turretRotationSpeed = 40;
    public Turret[] turrets;
    public EntityObject engine;
    public float radarGainLockRange = 40;
    public EntityObject radar;

    protected EntityObject[] weakspots;

    bool resetTurretArray;

    protected override void Awake()
    {
        base.Awake();

        weakspots = GetComponentsInChildren<EntityObject>().Where(obj => obj != this).ToArray();

        foreach (var weakspot in weakspots) {
            weakspot.OnShootedEvent += DealWeakDamage;
            weakspot.gameObject.layer = gameObject.layer;
            weakspot.tag = "WeakPoint";
        }

        foreach (var turret in turrets) {
            turret.defaultMaxRotationSpeed = turretRotationSpeed;
            turret.OnDestoryedEvent.AddListener(GetTurretArray);
        }

        defaultLockRange += radarGainLockRange;
        radar.OnDestoryedEvent.AddListener(DecreaseLockRange);
    }

    protected override void Update()
    {
        base.Update();
        if (resetTurretArray) {
            resetTurretArray = false;
            turrets = GetComponentsInChildren<Turret>();
        }

        if (Vector3.Distance(PlayerSpaceship.MainCharacter.transform.position, transform.position) < defaultLockRange) TryShoot();
    }

    protected override bool TryShoot()
    {
        bool res = false;
        foreach (var turret in turrets) {
            turret.PrepareShooting();
            res |= turret.LockingTarget;
        }
        return res;
    }

    private void DecreaseLockRange()
    {
        defaultLockRange -= radarGainLockRange;
    }

    private void GetTurretArray()
    {
        resetTurretArray = true;
    }

    private void DealWeakDamage(float damage)
    {
        ImpactDurability(magnification * damage);
    }

    protected override void OnDestoryed()
    {
        foreach (var weakspot in weakspots) {
            weakspot.OnShootedEvent -= DealWeakDamage;
        }
        base.OnDestoryed();
    }
}
