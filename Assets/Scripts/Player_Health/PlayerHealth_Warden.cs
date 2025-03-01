public class PlayerHealth_Warden : PlayerHealth
{
	protected override void TakeDamage(float damage, string player)
	{
		if (player.ToLower() != "warden") return;

		float newHealth = statsManager.WardenCurrentHealth - damage;

		if (newHealth > 0) statsManager.WardenCurrentHealth = newHealth;
		else
		{
			statsManager.WardenCurrentHealth = 0;
			Die();
		}

		healthBar.UpdateHealthBar(statsManager.WardenCurrentHealth, statsManager.WardenMaxHealth);
	}

	protected override void Die()
	{
		//send out signal
		EventManager.instance.playerEvents.PlayerDied();
		// TODO - implement death
		return;
	}
}
