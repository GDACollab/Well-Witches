using UnityEngine;
using System;
using UnityEngine.AI;
using System.Collections;
//using UnityEngine.InputSystem.Android; WHY WAS IT USING THIS

public class PlayerProjectile : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera cam;
    private Rigidbody2D rb;
    private Collider2D collider;

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject impact;

    private float damage;
    private float knockback;

    private Vector3 direction;

    private NavMeshAgent agent;

    public static event Action OnHitEnemy;

    public void InitializeProjectile(float velocity, float lifetime, float damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * velocity;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        projectile.SetActive(true);
        impact.SetActive(false);

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BaseEnemyClass>().TakeDamage(damage);
            if (collision != null)
            {
                collision.gameObject.GetComponent<BaseEnemyClass>().ProjectileKnockback(direction.normalized * knockback);
            }
            OnHitEnemy?.Invoke();
        }
        rb.velocity = Vector2.zero;
        rb.freezeRotation = true;
        projectile.SetActive(false);
        impact.SetActive(true);
        collider.enabled = false;
        Destroy(gameObject, 0.5f);
    }
}
