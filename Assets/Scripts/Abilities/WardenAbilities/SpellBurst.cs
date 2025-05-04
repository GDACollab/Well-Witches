using UnityEngine;
using System.Collections;

public class SpellBurst : MonoBehaviour
{
    [SerializeField] private SpellBurstProjectile projectilePrefab;

    public void Activate(float damage, float speed, float lifetime, float timeBetweenProjectile, float abilityDuration)
    {
        StartCoroutine(SpawnSpectralProjectile(damage, speed, lifetime, timeBetweenProjectile));
        Destroy(gameObject, abilityDuration);
    }

    IEnumerator SpawnSpectralProjectile(float damage, float speed, float lifetime, float timeBetweenProjectile)
    {
        while (true)
        {
            SpellBurstProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<SpellBurstProjectile>();
            projectile.Initialize(damage, speed, lifetime);
            yield return new WaitForSeconds(timeBetweenProjectile);
        }
    }
}

