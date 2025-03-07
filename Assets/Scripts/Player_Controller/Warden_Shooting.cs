using UnityEngine;
using UnityEngine.InputSystem;

public class Warden_Shooting : MonoBehaviour
{
	[SerializeField] float cooldown;
	[SerializeField] float velocity;
	[SerializeField] float lifetime;
	[SerializeField] GameObject prefab;
	[SerializeField] Transform spawn;
	InputAction inputAction;
	StatsManager statsManager;
	float cooldownCounter = 0;

	void Start()
	{
		inputAction = GetComponent<PlayerInput>().actions["Shoot"];
		statsManager = StatsManager.Instance;
	}

	void Update()
	{
		if (cooldownCounter > 0) cooldownCounter -= Time.deltaTime;
		if (inputAction.IsPressed() && cooldownCounter <= 0) Shoot(); 
	}

	void Shoot()
	{
		PlayerProjectile projectile = Instantiate(prefab, spawn.position, Quaternion.identity).GetComponent<PlayerProjectile>();
		projectile.InitializeProjectile(velocity, lifetime, statsManager.AttackPower);
		cooldownCounter = cooldown;
	}
}
