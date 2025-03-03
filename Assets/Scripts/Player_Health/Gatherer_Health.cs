public class Gatherer_Health : PlayerHealth
{
	protected override void TakeDamage(float damage, string player)
	{
		if (player.ToLower() != "gatherer") return;

		float newHealth = statsManager.GathererCurrentHealth - damage;

		if (newHealth > 0) statsManager.GathererCurrentHealth = newHealth;
		else
		{
			statsManager.GathererCurrentHealth = 0;
			Die();
		}

		healthBar.UpdateHealthBar(statsManager.GathererCurrentHealth, statsManager.GathererMaxHealth);
	}

	protected override void Die()
	{
		//send out signal
		EventManager.instance.playerEvents.PlayerDied();
		// TODO - implement death
		return;
	}
}
