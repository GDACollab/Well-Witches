using UnityEngine;
using System;


public class SpellBurstProjectile : MonoBehaviour
{
    [Header("Debug, Do Not Change")]
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private ParticleSystem impactSparks;
    [SerializeField] private ParticleSystem impactFlash;


    public static event Action OnHitEnemy;


    public void Initialize(Transform pivot, float damage, float speed, float lifetime)
    {
        transform.eulerAngles = new Vector3(0, 0, 90f);
        _damage = damage;
        _speed = speed;
        transform.SetParent(pivot);
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z +  _speed * 0.00625f);
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
