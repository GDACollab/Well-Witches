using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public float moveSpeed = 5f; // Speed of the player

    private Vector2 movement; // Stores the player's movement direction
    private Rigidbody2D rb; // Rigidbody component for physics-based movement

    void Start()
    {
        // Get the Rigidbody2D component attached to this GameObject
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found! Please attach a Rigidbody2D to the GameObject.");
        }
    }

    void Update()
    {
        // Get input from arrow keys
        float moveX = Input.GetAxisRaw("Horizontal"); // Left (-1) and Right (+1)
        float moveY = Input.GetAxisRaw("Vertical"); // Down (-1) and Up (+1)

        // Combine input into a movement vector
        movement = new Vector2(moveX, moveY).normalized; // Normalize to prevent faster diagonal movement
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody2D
        if (rb != null)
        {
            rb.velocity = movement * StatsManager.Instance.speed; //
            
        }
    }
}