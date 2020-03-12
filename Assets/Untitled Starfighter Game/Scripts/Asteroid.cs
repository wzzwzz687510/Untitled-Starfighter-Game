using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Status Setting")]
    public float defaultDurability = 100;

    public float Durability { get; protected set; }

    private Vector3 startScale;

    private void Start()
    {
        Durability = defaultDurability;
        startScale = transform.localScale;
    }

    public void ImpactDurability(float value)
    {
        Durability = Mathf.Clamp(Durability + value, 0, defaultDurability);
        transform.localScale = startScale * Durability / defaultDurability;
        if (Durability == 0) OnDestoryed();
    }

    private void OnDestoryed()
    {
        Destroy(gameObject);
    }
}
