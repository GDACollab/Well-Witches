using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	protected StatsManager statsManager;
	protected HealthBar healthBar;

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
		healthBar = GetComponentInChildren<HealthBar>();
		healthBar.UpdateHealthBar(statsManager.GathererCurrentHealth, statsManager.GathererMaxHealth);
	}

	protected virtual void TakeDamage(float damage, string player) { }  // to be implemented by the child class
	protected virtual void Die() { } // to be implemented by the child class
}
