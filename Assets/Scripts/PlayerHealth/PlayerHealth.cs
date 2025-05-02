using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	protected StatsManager statsManager;
	private Slider HealthSlider;
	public bool isInvulnerable = false;

	void OnEnable()     // Subscribe to events here
	{
		EventManager.instance.playerEvents.onPlayerDamage += TakeDamage;
	}
	void OnDisable()    // Unsubscribe to events here (otherwise we waste memory)
	{
		EventManager.instance.playerEvents.onPlayerDamage -= TakeDamage;
	}
	void Start()
	{
		statsManager = StatsManager.Instance;
		HealthSlider = GetComponentInChildren<Slider>();
	}
	public void UpdateHealthBar(float currentHealth, float maxHealth)
	{
		HealthSlider.value = currentHealth / maxHealth;
		//Debug.Log("should be working?");
	}

	protected virtual void TakeDamage(float damage, string player) { }  // to be implemented by the child class
	protected virtual void Die() { } // to be implemented by the child class
}
