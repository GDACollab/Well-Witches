using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	[SerializeField] Slider gathererAbilityMeter;
	[SerializeField] Slider wardenAbilityMeter;
	
	WardenBaseAbilities WardenAbility => WardenAbilityManager.Instance.equipedAbility;
	GathererBaseAbilities GathererAbility => GathererAbilityManager.Instance.equipedAbility;

	void OnEnable()     // Subscribe to events here
	{
		EventManager.instance.playerEvents.onPlayerDamage += HudHealthUpdate;
	}
	void OnDisable()    // Unsubscribe to events here (otherwise we waste memory)
	{
		EventManager.instance.playerEvents.onPlayerDamage -= HudHealthUpdate;
	}
	
	void Update()
	{
		// gathererAbilityMeter.value = (GathererAbilityManager.Instance != null) ? (GathererAbility.cooldown - GathererAbility.currentCooldown) / Mathf.Max(1, GathererAbility.cooldown) : 1;
		wardenAbilityMeter.value = (WardenAbilityManager.Instance != null) ? WardenAbility.Charge / Mathf.Max(1, WardenAbility.numHitsRequired) : 1;
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
