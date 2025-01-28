using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseEnemyClass : MonoBehaviour
{
    // Ranges for stats are just placeholders and can be changed.
    [Header("Enemy Stats")]
    [Range(1, 100)]
    [Tooltip("The max health of an enemy. [1, 100]")]
    public int health;
    [Range(0, 100)]
    [Tooltip("How much damage an enemy does. [0, 100]")]
    public int damage;
    [Range(0, 20)]
    [Tooltip("How fast an enemy moves. [0, 20]")]
    public int moveSpeed;

    // used for Move()
    Rigidbody2D rb;
    Vector3 click;
    Vector2 target;
    Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
    }

    /*
        Move towards mouse click
        Changing target can make this code work with A* pathfinding
    */
    void Move()
    {
        // convert position to Vector2 for comparison with target without z axis
        pos = new Vector2(transform.position.x, transform.position.y);
        // if left mouse button is clicked
        if (Input.GetMouseButtonDown(0)) {
            // get mouse click position
            click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target = new Vector2(click.x, click.y);
            // move towards target
            rb.velocity = (target - pos).normalized * moveSpeed;
        }
        // if enemy is close to target, stop moving
        if (Vector2.Distance(pos, target) < 0.5f) {
            rb.velocity = Vector2.zero;
        }

        // For the code below, assume the enemy sprite is facing left, the code is dependent on the direction of the enemy its facing
        // If direction.x (as calculated above) is positive, that means the enemy is on the left side of the player
        if (rb.velocity.x > 0)
        {
            // We dont make changes to the sprite since its already facing the player
            transform.localScale = new Vector3(1 ,1, 1);
        // If direction.x is negative (on the left of the player)
        } else if (rb.velocity.x < 0)
        {
            // We flip the enemy sprite so it faces the player
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void takingDamage(int amount)
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
