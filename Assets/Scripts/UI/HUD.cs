using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	[SerializeField] Slider gathererAbilityMeter;
	[SerializeField] Slider wardenAbilityMeter;
	StatsManager statsManager;
	Gatherer_FlashStun flashStun;
    WardenDevastationBeam bigBlast;

	void OnEnable()     // Subscribe to events here
	{
		EventManager.instance.playerEvents.onPlayerDamage += HudHealthUpdate;
	}
	void OnDisable()    // Unsubscribe to events here (otherwise we waste memory)
	{
		EventManager.instance.playerEvents.onPlayerDamage -= HudHealthUpdate;
	}
	void Awake()
	{
		statsManager = StatsManager.Instance;
		flashStun = Gatherer_FlashStun.Instance;
		bigBlast = WardenDevastationBeam.Instance;
	}
	void Update()
	{
		gathererAbilityMeter.value = (flashStun.cooldownDuration - flashStun.cooldownCounter) / flashStun.cooldownDuration;
		wardenAbilityMeter.value = (float)bigBlast.Charge / (float)bigBlast.NumHitsRequired;
	}

	void HudHealthUpdate(float damage, string player)
	{
		// if (player == "Gatherer")
		// {
		// 	statsManager.gathererHealthBar.UpdateHealthBar(statsManager.GathererCurrentHealth, statsManager.GathererMaxHealth);
		// }
		// else if (player == "Warden")
		// {
		// 	statsManager.wardenHealthBar.UpdateHealthBar(statsManager.WardenCurrentHealth, statsManager.WardenMaxHealth);
		// }
	}
}
