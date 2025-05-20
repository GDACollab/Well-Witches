using UnityEngine;
using UnityEngine.InputSystem;

public class Warden_Shooting : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float cooldown;
	[SerializeField] float velocity;
    [SerializeField] float knockback;
    [SerializeField] float lifetime;

    [Header("References")]
	[SerializeField] GameObject prefab;
	[SerializeField] Transform spawn;
	InputAction inputAction;
	StatsManager statsManager;
	float cooldownCounter = 0;
	public bool waterLog = false;

	void Start()
	{
		inputAction = WardenAbilityManager.Controls.asset["Shoot"];
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
		projectile.InitializeProjectile(velocity, lifetime, StatsManager.Instance.getAttackPower(), knockback);
		//AudioManager.Instance.PlayOneShot(FMODEvents.Instance.spectralShot, spawn.position);
		cooldownCounter = cooldown;
	}
}
