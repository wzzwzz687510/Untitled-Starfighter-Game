using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : Spaceship
{
    BoidSchool school;

    public void RegisterSchool(BoidSchool school)
    {
        this.school = school;
        InitializeStatus();
    }

    protected override void OnDestoryed()
    {
        base.OnDestoryed();
        school.RemoveBoid(GetComponent<Boid>());
        Destroy(gameObject);
    }

    protected override void Update()
    {
        base.Update();
        if (Vector3.Distance(PlayerSpaceship.MainCharacter.transform.position, transform.position) < defaultLockRange) Shoot();
    }

}
