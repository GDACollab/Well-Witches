using UnityEngine;

public class WardenShoot : MonoBehaviour
{
    [SerializeField] private StatsManager wardenStats;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawn;

    [SerializeField] private float projectileVelocity;
    [SerializeField] private float projectileLifetime;


    private void Awake()
    {
        // these should hopefully never trigger
        if (projectileSpawn == null) 
        {
            projectileSpawn = transform.Find("ProjectileSpawn");
        }
    }

    private void Update()
    {
        // if we are using the new input manager change this
        if (Input.GetMouseButtonDown(0))
        {
            PlayerProjectile projectile = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity).GetComponent<PlayerProjectile>();
            projectile.InitializeProjectile(projectileVelocity, projectileLifetime, wardenStats.AttackPower);
            
        }
    }
}

