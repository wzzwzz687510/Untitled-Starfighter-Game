using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : Spaceship
{
    BoidSchool school;

    protected override void Awake()
    {
        base.Awake();
    }

    public void RegisterSchool(BoidSchool school)
    {
        this.school = school;

        base.InitializeDefaultParameters();
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
        if (Vector3.Distance(PlayerSpaceship.MainCharacter.transform.position, transform.position) < defaultLockRange) TryShoot();
    }

}
