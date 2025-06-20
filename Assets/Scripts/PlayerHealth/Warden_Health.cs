using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Warden_Health : PlayerHealth
{
	
	public bool gourdForgeInvulnerability = false;
	protected override void TakeDamage(float damage, string player)
	{
		if (player.ToLower() != "warden") return;

		if (gourdForgeInvulnerability == true) {
			return;
		}

		float newHealth = statsManager.WardenCurrentHealth - damage;

		if (newHealth > 0)
		{
			statsManager.WardenCurrentHealth = newHealth;
            AudioManager.Instance.PlayPlayerHurt(player);
        }
		else if (newHealth <= 0 && statsManager.WardenCurrentHealth != 0) 
		{
			statsManager.WardenCurrentHealth = 0;
			Die();
		}

		UpdateHealthBar(statsManager.WardenCurrentHealth, statsManager.WardenMaxHealth);
	}

	protected override void Die()
	{
		base.Die();
		//send out signal
		EventManager.instance.playerEvents.PlayerDeath();
		WardenAbilityManager.Controls.Gameplay_Warden.Disable();
		AudioManager.Instance.PlayOneShot(FMODEvents.Instance.wardenDown, this.transform.position);
		return;
	}


}
