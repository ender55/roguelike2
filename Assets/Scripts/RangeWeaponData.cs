using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeaponData", menuName = "SO/Weapon Data/Range Weapon Data")]
public class RangeWeaponData : ScriptableObject //todo: handle with stats
{
    [SerializeField] private int projectilesPerShot;
    [SerializeField] private int projectileSpeed;
    
    //[SerializeField] private int projectilePiercePower;
    [SerializeField] private float spread;

    //[SerializeField] private float projectileSpeedModifier;
    //[SerializeField] private float spreadModifier;

    public int ProjectilesPerShot => projectilesPerShot;
    public int ProjectileSpeed => projectileSpeed;
    //public int ProjectilePiercePower => projectilePiercePower;
    public float Spread => spread;
    //public float ProjectileSpeedModifier => projectileSpeedModifier;
    //public float SpreadModifier => spreadModifier;
}