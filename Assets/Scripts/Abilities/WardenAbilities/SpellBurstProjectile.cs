using UnityEngine;
using System;

public class SpellBurstProjectile : MonoBehaviour
{
    [Header("Debug, Do Not Change")]
    [SerializeField] private float _damage;
    [SerializeField] private ParticleSystem impactSparks;
    [SerializeField] private ParticleSystem impactFlash;

    private Rigidbody2D rb;


    public static event Action OnHitEnemy;


    public void Initialize(float damage, float speed, float lifetime)
    {
        _damage = damage;
        rb = GetComponent<Rigidbody2D>();
        Vector3 randomTarget = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.value, UnityEngine.Random.value));

        Vector3 direction = randomTarget - transform.position;
        Vector3 rotation = transform.position - randomTarget;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<BaseEnemyClass>().TakeDamage(_damage);
            impactSparks.Play();
            impactFlash.Play();
            OnHitEnemy?.Invoke();
        }
    }
}
