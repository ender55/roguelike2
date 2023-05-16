using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[Serializable]
public struct Damage
{
    [SerializeField] private SerializedDictionary<DamageType, int> damage;

    public SerializedDictionary<DamageType, int> DamageValues => damage;
}
