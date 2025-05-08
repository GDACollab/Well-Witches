using UnityEngine;
using System.Collections;

public class SpellBurst : MonoBehaviour
{
    [SerializeField] private SpellBurstProjectile projectilePrefab;

    private Transform parent;
    private float timer;
    private float abilityDuration;

    public void Activate(float damage, float speed, float rotationForce, float lifetime, float timeBetweenProjectile, int projectilePerBurst, float abilityDuration)
    {
        parent = GameObject.Find("Warden").transform;
        this.abilityDuration = abilityDuration;
        timer = 0f;

        StartCoroutine(SpawnSpectralProjectile(damage, speed, rotationForce, lifetime, timeBetweenProjectile, projectilePerBurst));
    }

    private void Update()
    {
        timer += Time.deltaTime;
        transform.position = parent.position;
        if (transform.childCount == 0 && timer >= abilityDuration)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnSpectralProjectile(float damage, float speed, float rotationForce, float lifetime, float timeBetweenProjectile, int projectilePerBurst)
    {
        while (true)
        {
            for (int i = 1; i <= projectilePerBurst; i++)
            {
                Quaternion rotation = transform.rotation * Quaternion.Euler(0, 0, 360/i);
                SpellBurstProjectile projectile = Instantiate(projectilePrefab, transform.position, rotation).GetComponent<SpellBurstProjectile>();
                projectile.Initialize(transform, damage, speed, rotationForce, lifetime);
            }
            yield return new WaitForSeconds(timeBetweenProjectile);
        }
    }
}

