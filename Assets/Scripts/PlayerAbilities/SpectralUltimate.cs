using UnityEngine;

public class SpectralUltimate : MonoBehaviour
{
    public SpectralProjectile projectilePrefab;
    public Transform spawn;


    public float damage;
    public float projectileLifetime;


    void Start()
    {
        InvokeRepeating(nameof(SpawnSpectralProjectile), 0, 0.5f);
    }

    private void SpawnSpectralProjectile()
    {
        SpectralProjectile projectile = Instantiate(projectilePrefab, spawn.position, Quaternion.identity).GetComponent<SpectralProjectile>();
        //projectile.Initialize(damage, projectileLifetime);
    }
    
}
