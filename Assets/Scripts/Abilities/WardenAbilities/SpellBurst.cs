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
        transform.position = new Vector3(parent.position.x, parent.position.y + 0.5f, parent.position.z);
        if (transform.childCount == 1 && timer >= abilityDuration)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnSpectralProjectile(float damage, float speed, float rotationForce, float lifetime, float timeBetweenProjectile, int projectilePerBurst)
    {
        yield return new WaitForSeconds(0.5f);
        while (timer <= abilityDuration)
        {
            for (int i = 0; i < projectilePerBurst; i++)
            {
                Quaternion rotation = transform.rotation * Quaternion.Euler(0, 0, (360/projectilePerBurst) * i);
                Debug.Log(rotation.eulerAngles);
                SpellBurstProjectile projectile = Instantiate(projectilePrefab, transform.position, rotation).GetComponent<SpellBurstProjectile>();
                projectile.Initialize(transform, damage, speed, rotationForce, lifetime);
            }
            yield return new WaitForSeconds(timeBetweenProjectile);
        }
    }
}

