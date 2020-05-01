using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTurret : EntityObject
{
    [Header("Floating Turret")]
    public Turret[] childTurrets;
    public float turretRotationSpeed = 40;
    public float defaultLockRange = 80;

    protected override void Awake()
    {
        base.Awake();

        foreach (var turret in childTurrets) {
            turret.defaultMaxRotationSpeed = turretRotationSpeed;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (Vector3.Distance(PlayerSpaceship.MainCharacter.transform.position, transform.position) < defaultLockRange) TryShoot();
    }

    protected void TryShoot()
    {
        foreach (var turret in childTurrets) {
            turret.PrepareShooting();
        }
    }

    protected override void OnDestoryed()
    {
        MissionManager.Instance.OnSentryDestroyed();
        base.OnDestoryed();
    }
}
