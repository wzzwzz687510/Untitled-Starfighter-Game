using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Spaceship
{
    [Header("Turret Parameters")]
    public float detectRange = 100;

    private Vector3 predictionTarget;
    private float distance;
    private float angle;

    private void Awake()
    {
        InitializeStatus();
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, PlayerSpaceship.MainCharacter.transform.position);
        if (distance < detectRange) {
            //Debug.Log("Player Enter Attack Zone");
            TryShoot();
        }        
    }

    private void TryShoot()
    {
        predictionTarget = PlayerSpaceship.MainCharacter.transform.position + 
            PlayerSpaceship.MainCharacter.transform.forward * 
            PlayerSpaceship.MainCharacter.Controller.CurrentSpeed *
            (distance/(SelectedEquipmentObject.Template as Weapon).bullet.speed);

        Vector3 direction = (predictionTarget - transform.position).normalized;
        angle = Vector3.Angle(direction, transform.forward);

        if (angle < 1) Shoot();
        else {
            var lookatDir = Vector3.RotateTowards(transform.forward, direction, defaultMaxRotationSpeed * Time.deltaTime / angle,1);
            transform.LookAt(transform.position+lookatDir);
        }
    }

    protected override void OnDestoryed()
    {
        base.OnDestoryed();
        Destroy(gameObject);
    }
}
