using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentObject
{
    public int TemplateHash { get; private set; }
    public Equipment Template => TemplateHash.GetEquipment();

    public float Volume { get; private set; }
    public float Durability { get; private set; }
    public float Timer { get; private set; }
    public bool VolumeInfinite { get; private set; }
    public bool Reloading { get; private set; }
    public bool Triggerable => !Reloading && (Volume > 0 || VolumeInfinite) && Timer <= 0;

    

    public EquipmentObject(int hash)
    {
        InitializeEquipment(hash);
    }

    public void InitializeEquipment(int hash)
    {
        TemplateHash = hash;

        Equipment eq = hash.GetEquipment();
        if (eq.volume == 0) VolumeInfinite = true;
        Volume = eq.volume;
        Durability = eq.durability;
    }

    public void UpdateVolume(float value)
    {
        Volume += value;
    }

    public void UpdateTimer(float value)
    {
        Timer += value;
    }

    public void ResetTimer()
    {
        Timer = Template.triggerInterval;
    }

    public void ResetVolume()
    {
        Volume = Template.volume;
    }

    public void SetReload(bool bl)
    {
        Reloading = bl;        
    }
}
