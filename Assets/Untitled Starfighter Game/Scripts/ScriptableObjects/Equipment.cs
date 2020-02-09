using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Laser
}

public abstract class Equipment : ScriptableObject
{
    public string equipmentName;
    public int Hash => equipmentName.GetStableHashCode();

    public EquipmentType type;

    // Maximum reload bullets number.(0 means infinate)
    public float volume;
    // Maximum equipment use time.(0 means infinate)
    public float durability;
    // Interval of impact per trigger.
    public float triggerInterval = 0.1f;
    // Duration of reload.
    public float reloadDuration = 3;

    static Dictionary<int, Equipment> cache;
    public static Dictionary<int,Equipment> Dict {
        get {
            return cache ?? (cache = Resources.LoadAll<Equipment>("ScriptableObject").ToDictionary(
            item => item.Hash, item => item)
            );
        }
    }
}
