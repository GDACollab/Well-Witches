using UnityEngine;

public class Gatherer_Health : PlayerHealth
{
	protected override void TakeDamage(float damage, string player)
	{
		if (player.ToLower() != "gatherer") return;

		/** Bubble Shield Damage Prevention
		* Since projectiles are hard coded to check for bubble shield and bounce off,
		* any damage gatherer takes should be the result of melee damage.
		* When the shield hit the shield should break but no damage should be done.
		*/


		float newHealth = statsManager.GathererCurrentHealth - damage;

		if (newHealth > 0) statsManager.GathererCurrentHealth = newHealth;
		else
		{
			statsManager.GathererCurrentHealth = 0;
			Die();
		}

		UpdateHealthBar(statsManager.GathererCurrentHealth, statsManager.GathererMaxHealth);
	}

	protected override void Die()
	{
		//send out signal
		EventManager.instance.playerEvents.PlayerDeath();
		SceneHandler.Instance.ToHubScene();
		AudioManager.Instance.PlayOneShot(FMODEvents.Instance.gathererDown, GameObject.Find("Gatherer").transform.position);
		return;
	}
}
