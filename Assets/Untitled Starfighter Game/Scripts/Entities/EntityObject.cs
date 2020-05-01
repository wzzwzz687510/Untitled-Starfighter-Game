using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct VisualEffects
{
    public GameObject highlight;
    public GameObject destoryEffect;
}

public class EntityObject : MonoBehaviour
{
    [Header("Status Setting")]
    public float defaultDurability = 100;
    public float defaultArmour = 0;
    public VisualEffects vfx;

    public float MaxDurability { get; protected set; }
    public float Durability { get; protected set; }
    public float MaxArmour { get; protected set; }
    public float Armour { get; protected set; }
    public bool IsDestroyed { get; protected set; }
    public bool Invincible { get; protected set; }

    public delegate void ValueChangeDelegate(float curV, float maxV);
    public ValueChangeDelegate OnDurabilityChangedEvent;
    public ValueChangeDelegate OnAmourChangeEvent;
    public delegate void SingleValueDelegate(float value);
    public SingleValueDelegate OnShootedEvent;
    [HideInInspector] public UnityEvent OnDestoryedEvent;

    protected virtual void Awake()
    {
        InitializeDefaultParameters();
    }

    protected virtual void Update()
    {

    }

    protected virtual void Start()
    {
        InitializeAfterAwakeParameters();
    }

    protected virtual void InitializeDefaultParameters()
    {
        MaxDurability = defaultDurability;
        Durability = defaultDurability;

        MaxArmour = defaultArmour;
        Armour = defaultArmour;
    }

    protected virtual void InitializeAfterAwakeParameters()
    {

    }

    public virtual void OnShooted(Vector3 direction, float damage)
    {
        ImpactDurability(-Mathf.Abs(damage));
        OnShootedEvent?.Invoke(-Mathf.Abs(damage));
    }

    public virtual void ImpactDurability(float value)
    {
        if (IsDestroyed || Invincible) return;
        Durability = Mathf.Clamp(Durability + value, 0, defaultDurability);
        OnDurabilityChangedEvent?.Invoke(Durability, defaultDurability);
        if (Durability == 0) OnDestoryed();
    }

    public virtual void ImpactArmour(float value)
    {
        if (IsDestroyed || Invincible) return;
        Armour = Mathf.Clamp(Armour + value, 0, defaultArmour);
        OnAmourChangeEvent?.Invoke(Armour, defaultArmour);
    }

    public virtual void SetHighlight(bool bl)
    {
        if (vfx.highlight)
            vfx.highlight.SetActive(bl);
    }

    protected virtual void OnDestoryed()
    {
        IsDestroyed = true;
        if (vfx.destoryEffect)
            Instantiate(vfx.destoryEffect, transform.position, Quaternion.identity).transform.localScale *= 10;
        AudioManager.Instance.PlayExplosionClip();
        OnDestoryedEvent?.Invoke();
        Destroy(gameObject);
    }

}
