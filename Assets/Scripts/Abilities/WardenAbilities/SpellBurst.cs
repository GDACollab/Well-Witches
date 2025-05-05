using UnityEngine;
using System.Collections;

public class SpellBurst : MonoBehaviour
{
    [SerializeField] private SpellBurstProjectile projectilePrefab;

    private Transform parent;

    public void Activate(float damage, float speed, float lifetime, float timeBetweenProjectile, float abilityDuration)
    {
        parent = GameObject.Find("Warden").transform;
        StartCoroutine(SpawnSpectralProjectile(damage, speed, lifetime, timeBetweenProjectile));
        Destroy(gameObject, abilityDuration);
    }

    private void Update()
    {
        transform.position = parent.position; 
        transform.Rotate(Vector3.forward, 2f);
    }

    IEnumerator SpawnSpectralProjectile(float damage, float speed, float lifetime, float timeBetweenProjectile)
    {
        while (true)
        {
            SpellBurstProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<SpellBurstProjectile>();
            projectile.Initialize(transform, damage, speed, lifetime);
            yield return new WaitForSeconds(timeBetweenProjectile);
        }
    }
}

