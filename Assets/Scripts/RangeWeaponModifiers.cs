using System;
using UnityEngine;

[Serializable]
public class RangeWeaponModifiers
{
    [Min(0.1f)][SerializeField] private float projectileSizeModifier = 1f;
    [Min(0.1f)][SerializeField] private float spreadModifier = 1f;

    public float ProjectileSizeModifier
    {
        get => projectileSizeModifier;
        set
        {
            if (value > 0)
            {
                projectileSizeModifier = value;
            }
        }
    }

    public float SpreadModifier
    {
        get => spreadModifier;
        set
        {
            if (value > 0)
            {
                spreadModifier = value;
            }
        }
    }
}
