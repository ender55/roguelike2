using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "SO/Weapon Data/Weapon Data")]
public class WeaponData : ScriptableObject //todo: clear code
{
    //stats
    [SerializeField] private Damage baseDamage;
    [SerializeField] private float attackSpeed;
    //[SerializeField] private float energyConsumption;
    //[SerializeField] private float knockback;
    //[SerializeField] private int criticalDamageChance;
    //[SerializeField] private float criticalDamageMultiplier;
    //[SerializeField] private float attackRange;
    
    //modifiers
    [SerializeField] private float damageModifier;
    [SerializeField] private float attackSpeedModifier;
    //[SerializeField] private float energyConsumptionModifier;

    public Damage BaseDamage => baseDamage;
    public float AttackSpeed => attackSpeed;
    //public float EnergyConsumption => energyConsumption;
    //public float Knockback => knockback;
    //public int CriticalDamageChance => criticalDamageChance;
    //public float CriticalDamageMultiplier => criticalDamageMultiplier;
    //public float AttackRange => attackRange;
    public float DamageModifier
    {
        get => damageModifier;
        set => damageModifier = value;
    }

    public float AttackSpeedModifier => attackSpeedModifier;
    //public float EnergyConsumptionModifier => energyConsumptionModifier;
}