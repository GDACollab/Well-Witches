using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemyClass : MonoBehaviour
{
    [Header("Enemy Stats")]
    [Tooltip("The max health of an enemy.")]
    public float health;
    [Tooltip("How fast an enemy moves.")]
    public float moveSpeed;
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;

    public NavMeshAgent agent;

    public Rigidbody2D rb;

    public void Spawn(Vector3 position)
    {
        Instantiate(gameObject, position, Quaternion.identity);
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        EnemySpawner.currentEnemyCount--;
        Destroy(gameObject);
    }

    public virtual void ProjectileKnockback(Vector3 force)
    {
        agent.enabled = false;
        rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(EnableAgent());
    }
    
    IEnumerator EnableAgent()
    {
        yield return new WaitForSeconds(0.2f);
        agent.enabled = true;
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector3.zero;
    }

}
