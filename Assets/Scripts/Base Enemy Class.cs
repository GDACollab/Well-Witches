using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemyClassScript : MonoBehaviour
{
    //Below are the enemy properties
    public float health = 100f;
    public float damage = 100f;
    public float movespeed = 100f;
    public Transform playerTransform; //holds value to the player, such as its position and speed
    public Transform enemyTransforms; //holds value to the enemy
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyTransforms = transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {   //Normalize the vector to scale its magnitude to 1, so the movement speed remains constant regardless of enemy's distant from the player
            Vector2 direction = (playerTransform.position - enemyTransforms.position).normalized; 
            //Move the enemy at the direction of the player (if that's what the design department want the enemy to behave, can be modified later)
            rb.MovePosition(rb.position + direction * movespeed * Time.fixedDeltaTime);


            // For the code below, assume the enemy sprite is facing left, the code is dependent on the direction of the enemy its facing
            // If direction.x (as calculated above) is positive, that means the enemy is on the left side of the player
            if (direction.x > 0)
                // We dont make changes to the sprite since its already facing the player
                enemyTransforms.localScale = new Vector3(1 ,1, 1);
            // If direction.x is negative (on the left of the player)
            else if (direction.x < 0)
                // We flip the enemy sprite so it faces the player
                enemyTransforms.localScale = new Vector3(-1, 1, 1);
        }
        
    }

    public void takingDamage(float amount)
    {   //Reduces health by the amount entered in Unity
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("The object has died");
        Destroy(gameObject);
    }
    

}