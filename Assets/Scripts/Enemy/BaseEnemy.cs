using UnityEngine;

public class BaseEnemyClass : MonoBehaviour
{
    // Ranges for stats are just placeholders and can be changed.
    [Header("Enemy Stats")]
    [Range(1, 100)]
    [Tooltip("The max health of an enemy. [1, 100]")]
    public float health;
    [Range(0, 20)]
    [Tooltip("How fast an enemy moves. [0, 20]")]
    public float moveSpeed;


    public void Spawn(Vector3 position)
    {
        Instantiate(gameObject, position, Quaternion.identity);
    }

    public void TakeDamage(float amount)
    {   //Reduces health by the amount entered in Unity
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
