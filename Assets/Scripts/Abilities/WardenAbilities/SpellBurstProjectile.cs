using UnityEngine;
using System;

public class SpellBurstProjectile : MonoBehaviour
{
    [Header("Debug, Do Not Change")]
    [SerializeField] private float damage;
    [SerializeField] private ParticleSystem impactSparks;
    [SerializeField] private ParticleSystem impactFlash;

    private Rigidbody2D rb;
    private float speed;
    [SerializeField] private float rotationForce;
    private Transform pivot;

    public static event Action OnHitEnemy;


    public void Initialize(Transform pivot, float damage, float speed, float rotationForce, float lifetime)
    {
        this.damage = damage;
        this.pivot = pivot;
        this.speed = speed;
        this.rotationForce = rotationForce;
        rb = GetComponent<Rigidbody2D>();

        transform.SetParent(pivot);

        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
        transform.Rotate(Vector3.forward, rotationForce * Time.fixedDeltaTime);
        rotationForce *= 0.995f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<BaseEnemyClass>().TakeDamage(damage);
            impactSparks.Play();
            impactFlash.Play();
            OnHitEnemy?.Invoke();
        }
    }
}
