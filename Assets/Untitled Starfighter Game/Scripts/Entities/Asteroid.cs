using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Status Setting")]
    public float defaultDurability = 100;
    [Range(0, 5)] public float richDegree = 1.0f;
    public ParticleSystem particle;

    public float Durability { get; protected set; }

    private Vector3 startScale;
    private bool destorying;

    public void Initialize(float durability)
    {
        defaultDurability = durability;
        Durability = defaultDurability;
        startScale = transform.localScale;
    }

    public float ImpactDurability(float value)
    {
        float tmp = Durability;
        if (!destorying && Durability < 0.2 * defaultDurability) {
            StartCoroutine(OnDestoryed());
        }
        else {
            Durability = Mathf.Clamp(Durability + value, 0, defaultDurability);
        }
        transform.localScale = startScale * Durability / defaultDurability;
        return (tmp - Durability) * richDegree;
    }

    private IEnumerator OnDestoryed()
    {
        destorying = true;
        Durability = 0;
        gameObject.layer = 0;
        particle.Play();
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
