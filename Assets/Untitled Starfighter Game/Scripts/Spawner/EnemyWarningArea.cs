using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarningArea : BoidSchool
{
    public float detectRange = 50;

    bool spawned;

    protected override void Awake()
    {
        base.Awake();
        spawnOnAwake = false;
    }

    private void FixedUpdate()
    {
        if (!spawned && Vector3.SqrMagnitude(PlayerSpaceship.MainCharacter.transform.position - transform.position) < detectRange * detectRange) {
            spawned = true;
            InitializeSchool();          
        }
    }
}
