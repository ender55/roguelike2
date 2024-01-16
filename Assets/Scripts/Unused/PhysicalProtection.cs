using System;
using UnityEngine;

[Serializable]
public class PhysicalProtection
{
    [SerializeField] private int protectionValue;
    private float _protectionMultiplier;

    public int ProtectionValue
    {
        get => protectionValue;
        set
        {
            protectionValue = value;
            _protectionMultiplier = 1 - (float)protectionValue / (protectionValue + 20);
            OnProtectionValueChange?.Invoke();
        }
    }
    public float ProtectionMultiplier => _protectionMultiplier;

    public event Action OnProtectionValueChange;

    public void Init()
    {
        _protectionMultiplier = 1 - (float)protectionValue / (protectionValue + 20);
    }
}
