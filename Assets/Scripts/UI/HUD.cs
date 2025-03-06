using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	[SerializeField] Slider gathererHealthBar;
	[SerializeField] Slider gathererAbilityMeter;
	[SerializeField] Slider wardenHealthBar;
	[SerializeField] Slider wardenAbilityMeter;
	StatsManager statsManager;
	Gatherer_FlashStun flashStun;
	Warden_BigBlast bigBlast;

	void OnEnable()     // Subscribe to events here
	{
		EventManager.instance.playerEvents.onPlayerDamaged += OnPlayerDamaged;
	}
	void OnDisable()    // Unsubscribe to events here (otherwise we waste memory)
	{
		EventManager.instance.playerEvents.onPlayerDamaged -= OnPlayerDamaged;
	}
	void Start()
	{
		statsManager = StatsManager.Instance;
		flashStun = Gatherer_FlashStun.Instance;
		bigBlast = Warden_BigBlast.Instance;
	}
	void Update()
	{
		gathererAbilityMeter.value = (flashStun.cooldownDuration - flashStun.cooldownCounter) / flashStun.cooldownDuration;
		wardenAbilityMeter.value = (float)bigBlast.Charge / (float)bigBlast.NumHitsRequired;
	}

	void OnPlayerDamaged(float damage, string player)
	{
		if (player.ToLower() == "gatherer")
		{
			float newHealth = statsManager.GathererCurrentHealth - damage;

			if (newHealth > 0) statsManager.GathererCurrentHealth = newHealth;
			else
			{
				statsManager.GathererCurrentHealth = 0;
				EventManager.instance.playerEvents.PlayerDied();
			}

			gathererHealthBar.value = newHealth / statsManager.GathererMaxHealth;
		}

		else if (player.ToLower() == "warden")
		{
			float newHealth = statsManager.WardenCurrentHealth - damage;

			if (newHealth > 0) statsManager.WardenCurrentHealth = newHealth;
			else
			{
				statsManager.WardenCurrentHealth = 0;
				EventManager.instance.playerEvents.PlayerDied();
			}

			wardenHealthBar.value = newHealth / statsManager.WardenMaxHealth;
		}
	}
}
