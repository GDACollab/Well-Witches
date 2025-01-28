using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Vars")]
    public float moveSpeed = 5f; // Speed of the player

    private Vector2 movement; // Stores the player's movement direction
    private Rigidbody2D rb; // Rigidbody component for physics-based movement

    public enum playerType {WARDEN,GATHERER};
    [Header("Two Player Movement")]
    [SerializeField] public playerType movementType = playerType.GATHERER;

    [Header("References")]
    [SerializeField] public GameObject gatherer = null;
    [SerializeField] public SpringJoint2D springJoint = null;
    
    private float moveY;
    private float moveX;

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
        if (movementType == playerType.GATHERER)
        {
            moveX = Input.GetAxisRaw("Horizontal"); // Left (-1) and Right (+1)
            moveY = Input.GetAxisRaw("Vertical"); // Down (-1) and Up (+1)
        }
        else
        {
            moveX = Input.GetAxisRaw("ArrowHorizontal"); // Left (-1) and Right (+1)
            moveY = Input.GetAxisRaw("ArrowVertical"); // Down (-1) and Up (+1)
            springJoint.connectedAnchor = gatherer.transform.InverseTransformPoint(gatherer.transform.position);
            Debug.Log(gatherer.transform.position);
            springJoint.anchor = transform.InverseTransformPoint(transform.position);
        }


        // Combine input into a movement vector
        movement = new Vector2(moveX, moveY).normalized; // Normalize to prevent faster diagonal movement
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody2D
            rb.velocity = movement * moveSpeed;
    }
}