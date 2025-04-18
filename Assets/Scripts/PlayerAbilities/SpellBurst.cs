using UnityEngine;
using System.Collections;

public class SpellBurst: MonoBehaviour
{
    [Tooltip("Target should be Warden.")]
    [SerializeField] private Transform target;
    [SerializeField] private SpellBurstProjectile projectilePrefab;

    private float speed;

    private void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Warden").GetComponent<Transform>();   
        }
    }

    public void Activate(float damage, float speed, float distance, float lifetime, float timeBetweenProjectile, float abilityDuration)
    {
        this.speed = speed;
        StartCoroutine(SpawnSpectralProjectile(damage, speed, distance, lifetime, timeBetweenProjectile));
        Destroy(gameObject, abilityDuration);
    }

    IEnumerator SpawnSpectralProjectile(float damage, float speed, float distance, float lifetime, float timeBetweenProjectile)
    {
        while (true)
        {
            SpellBurstProjectile projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + distance, transform.position.z), Quaternion.identity).GetComponent<SpellBurstProjectile>();
            projectile.Initialize(transform, damage, speed, lifetime);
            yield return new WaitForSeconds(timeBetweenProjectile);
        }
        
    }

    private void Update()
    {
        transform.position = target.position;
        transform.Rotate(Vector3.forward, speed);
    }
}
