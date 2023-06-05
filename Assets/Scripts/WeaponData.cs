using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "SO/Weapon Data/Weapon Data")]
public class WeaponData : ScriptableObject //todo: clear code
{
    [SerializeField] private Damage baseDamage;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int energyConsumption;
    //[SerializeField] private float knockback;
    //[SerializeField] private int criticalDamageChance;
    //[SerializeField] private float criticalDamageMultiplier;
    //[SerializeField] private float attackRange;

    public Damage BaseDamage => baseDamage;
    public int AttackSpeed => attackSpeed;
    public int EnergyConsumption => energyConsumption;
    //public float Knockback => knockback;
    //public int CriticalDamageChance => criticalDamageChance;
    //public float CriticalDamageMultiplier => criticalDamageMultiplier;
    //public float AttackRange => attackRange;
    //public float EnergyConsumptionModifier => energyConsumptionModifier;
}