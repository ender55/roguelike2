using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Upgrade/Weapon Upgrade/Fire Upgrade", fileName = "FireUpgrade")]
public class BurningWeaponUpgrade : GeneralWeaponUpgrade
{
    [SerializeField] private SerializedDictionary<UpgradeRarity, int> burningTimeByRarity;
    [SerializeField] private SerializedDictionary<int, int> burningDamageByLevel;
        
    public override void Activate()
    {
        currentWeapon.OnHit += ApplyBurningState;
    }

    public override void Deactivate()
    {
        currentWeapon.OnHit -= ApplyBurningState;
    }

    private void ApplyBurningState(IDamageable damageable)
    {
        if (damageable is IStateHandler stateHandler)
        {
            if (stateHandler.StateMachine.HasState<BurningState>())
            {
                stateHandler.StateMachine.DeleteState<BurningState>();
                stateHandler.StateMachine.SetState(new BurningState(damageable, burningTimeByRarity[currentUpgradeRarity], burningDamageByLevel[currentUpgradeLevel]));
            }
            else
            {
                stateHandler.StateMachine.SetState(new BurningState(damageable, burningTimeByRarity[currentUpgradeRarity], burningDamageByLevel[currentUpgradeLevel]));
            }
        }
    }
}
