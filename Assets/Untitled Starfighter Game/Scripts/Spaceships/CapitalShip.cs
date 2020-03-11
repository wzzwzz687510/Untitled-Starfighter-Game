using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CapitalShip : Spaceship
{
    public Spaceship[] weakspots;

    public void Start()
    {
        InitializeStatus();

        foreach (var weakspot in weakspots) {
            weakspot.OnDeathEvent.AddListener(CheckDeathCondition);
        }
    }

    private void CheckDeathCondition()
    {        
        bool alive = false;
        foreach (var weakspot in weakspots) {
            if (!weakspot.IsDeath) alive = true;
        }
        if (!alive) OnDestoryed();
    }

    protected override void OnDestoryed()
    {
        base.OnDestoryed();
        Instantiate(effects.destoryEffect, transform.position, Quaternion.identity).transform.localScale *= 10;
        Destroy(gameObject);
    }
}
