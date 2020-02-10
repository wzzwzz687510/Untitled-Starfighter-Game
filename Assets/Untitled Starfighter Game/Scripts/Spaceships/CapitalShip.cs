using UnityEngine;

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
}
