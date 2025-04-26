using UnityEngine;

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
        Destroy(gameObject);
    }

}
