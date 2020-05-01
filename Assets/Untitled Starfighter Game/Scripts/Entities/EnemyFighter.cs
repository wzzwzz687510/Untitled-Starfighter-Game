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
        school.RemoveBoid(GetComponent<Boid>());
        if(school.Boids.Count == 0) {
            MissionManager.Instance.OnDestroyAllDefenders();
        }
        base.OnDestoryed();
    }

    protected override void Update()
    {
        base.Update();
        if (Vector3.Distance(PlayerSpaceship.MainCharacter.transform.position, transform.position) < defaultLockRange) TryShoot();
    }

}
