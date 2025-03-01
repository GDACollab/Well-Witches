using UnityEngine;

public class WardenShoot : MonoBehaviour
{
    [SerializeField] float projectileVelocity;
    [SerializeField] float projectileLifetime;
	[SerializeField] GameObject projectilePrefab;
	[SerializeField] Transform projectileSpawn;
    StatsManager statsManager;

	void Start()
	{
		statsManager = StatsManager.Instance;
		if (projectileSpawn == null) projectileSpawn = transform.Find("ProjectileSpawn");   // this ideally should never trigger
	}

	void OnShoot()  // called by the Player Input component
    {
		PlayerProjectile projectile = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity).GetComponent<PlayerProjectile>();
		projectile.InitializeProjectile(projectileVelocity, projectileLifetime, statsManager.AttackPower);
	}
}

