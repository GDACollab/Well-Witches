using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider healthbar;

	void Awake()
	{
		healthbar = GetComponentInChildren<Slider>();
	}

	public void UpdateHealthBar(float currentHealth, float maxHealth)
	{
		healthbar.value = currentHealth / maxHealth;
    }
}
