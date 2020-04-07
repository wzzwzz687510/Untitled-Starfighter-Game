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

    protected override void Awake()
    {
        base.Awake();

        weakspots = GetComponentsInChildren<EntityObject>().Where(obj => obj != this).ToArray();

        foreach (var weakspot in weakspots) {
            weakspot.OnDurabilityChangedEvent += DealWeakDamage;
            weakspot.gameObject.layer = gameObject.layer;
            weakspot.tag = "WeakPoint";
        }

        foreach (var turret in turrets) {
            turret.defaultMaxRotationSpeed = turretRotationSpeed;
        }

        defaultLockRange += radarGainLockRange;
        radar.OnDestoryedEvent.AddListener(DecreaseLockRange);
    }

    protected override void Update()
    {
        base.Update();

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

    private void DealWeakDamage(float value,float maxDurability)
    {
        ImpactDurability(magnification * value);
    }

    protected override void OnDestoryed()
    {
        base.OnDestoryed();
        Instantiate(particleEffects.destoryEffect, transform.position, Quaternion.identity).transform.localScale *= 10;
        foreach (var weakspot in weakspots) {
            weakspot.OnDurabilityChangedEvent -= DealWeakDamage;
        }
        Destroy(gameObject);
    }
}
