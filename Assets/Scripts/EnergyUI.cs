using TNRD;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private SerializableInterface<IEnergyUser> energyUser;
    [SerializeField] private Slider energySlider;

    private void OnEnable()
    {
        UpdateUI();
        energyUser.Value.Energy.OnEnergyChange += UpdateUI;
        energyUser.Value.Energy.OnMaxEnergyChange += UpdateUI;
    }

    private void OnDisable()
    {
        energyUser.Value.Energy.OnEnergyChange -= UpdateUI;
        energyUser.Value.Energy.OnMaxEnergyChange -= UpdateUI;
    }

    private void UpdateUI()
    {
        var sliderValue = (float)energyUser.Value.Energy.CurrentEnergy / energyUser.Value.Energy.MaxEnergy;
        energySlider.value = sliderValue;
    }
}
