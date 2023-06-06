using TNRD;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private SerializableInterface<IDamageable> healthUser;
    [SerializeField] private Slider healthSlider; 
    
    private void OnEnable()
    {
        UpdateUI();
        healthUser.Value.Health.OnHpChange += UpdateUI;
        healthUser.Value.Health.OnMaxHpChange += UpdateUI;
    }

    private void OnDisable()
    {
        healthUser.Value.Health.OnHpChange -= UpdateUI;
        healthUser.Value.Health.OnMaxHpChange -= UpdateUI;
    }

    private void UpdateUI()
    {
        var sliderValue = (float)healthUser.Value.Health.CurrentHp / healthUser.Value.Health.MaxHp;
        healthSlider.value = sliderValue;
    }
}
