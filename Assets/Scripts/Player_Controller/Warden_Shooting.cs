using UnityEngine;

public class Warden_Shooting : MonoBehaviour
{
	[SerializeField] float cooldown;
	[SerializeField] float velocity;
	[SerializeField] float lifetime;
	[SerializeField] GameObject prefab;
	[SerializeField] Transform spawn;
	float cooldownCounter = 0;
	StatsManager statsManager;

	void Start()
	{
		statsManager = StatsManager.Instance;
	}

	void Update()
	{
		if (cooldownCounter > 0) cooldownCounter -= Time.deltaTime;
	}

	void OnShoot()  // called by the Player Input component
	{
		if (cooldownCounter > 0) return;

		PlayerProjectile projectile = Instantiate(prefab, spawn.position, Quaternion.identity).GetComponent<PlayerProjectile>();
		projectile.InitializeProjectile(velocity, lifetime, statsManager.AttackPower);
		cooldownCounter = cooldown;
	}
}
