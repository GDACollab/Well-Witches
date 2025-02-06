using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Vars")]

    public float moveSpeed = 5f; // Speed of the player
    private Vector2 movement; // Stores the player's movement direction
    public enum playerType { WARDEN, GATHERER };

    [Header("Two Player Movement")]

    [SerializeField] public playerType movementType = playerType.GATHERER;
    [Tooltip("Changes how 'stiff' the rope is, only editable on the Warden player object")]
    [SerializeField] public float ropeStrength = 2.0f;
    private bool inRange = true;


    [Header("References")]

    [SerializeField] public GameObject gatherer = null;
    [SerializeField] public SpringJoint2D springJoint = null;
    private CircleCollider2D wardenRadiusCollider;
    private Rigidbody2D rb; // Rigidbody component for physics-based movement

    private float moveY;
    private float moveX;

    void Start()
    {
        // Get the Rigidbody2D component attached to this GameObject
        rb = GetComponent<Rigidbody2D>();

        if (movementType == playerType.GATHERER) // Gatherer-Only part of the Start function
        {
            wardenRadiusCollider = GetComponent<CircleCollider2D>();
            if (wardenRadiusCollider == null)
            {
                Debug.LogError("Warden radius CircleCollider2D not found! Please attatch a CircleCollider2D to the Gatherer GameObject");
            }
        }
        else // Warden-Only part of the Start function
        {
            springJoint.frequency = ropeStrength;
            springJoint.enabled = false;
            springJoint.distance = gatherer.GetComponent<CircleCollider2D>().radius;
        }

        wardenRadiusCollider = GetComponent<CircleCollider2D>();

        //warnings
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found! Please attach a Rigidbody2D to the GameObject.");
        }


    }

    void Update()
    {
        moveX = Input.GetAxisRaw("ArrowHorizontal"); // Left (-1) and Right (+1)
        moveY = Input.GetAxisRaw("ArrowVertical"); // Down (-1) and Up (+1)
        springJoint.connectedAnchor = gatherer.transform.position;
        springJoint.anchor = transform.InverseTransformPoint(transform.position);



        // Combine input into a movement vector
        movement = new Vector2(moveX, moveY).normalized; // Normalize to prevent faster diagonal movement
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody2D
        rb.velocity = movement * moveSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (movementType == playerType.WARDEN)
        {
            inRange = true;
            springJoint.enabled = false;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (movementType == playerType.WARDEN)
        {
            inRange = false;
            springJoint.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider,collision.otherCollider);
        }

    }
}

    