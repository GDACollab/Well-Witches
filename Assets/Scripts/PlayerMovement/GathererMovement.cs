using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererMovement : MonoBehaviour
{


    public PlayerMovementData moveData; //Scriptable object that holds all our movement vars

    //Components
    public Rigidbody2D rb;

    //Input
    //TO-DO: Change to new input system
    private Vector2 playerInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    //Update is not framerate independant, so use it to grab inputs
    private void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


    }

    //fixedUpdate is framerate independant, so do physics calculations here
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //Calculate direction + desired velocity
        Vector2 targetSpeed = playerInput * moveData.moveSpeed;

        float accelRate = (Mathf.Abs(targetSpeed.x) > 0.01f && Mathf.Abs(targetSpeed.y) > 0.01f) ? moveData.accelAmount : moveData.decelAmount;

        //Conserve Momentumn
        if (moveData.conserveMomentum && rb.velocity.magnitude > targetSpeed.magnitude && Vector2.Dot(rb.velocity.normalized,targetSpeed.normalized) == 1)
        {
            accelRate = 0;
        }
        Vector2 speedDif = targetSpeed - rb.velocity;

        Vector2 movement = speedDif * accelRate;

        rb.AddForce(movement,ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            WardenMovement warden = collision.gameObject.GetComponent<WardenMovement>();
            warden.disableRope();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            WardenMovement warden = collision.gameObject.GetComponent<WardenMovement>();
            warden.enableRope();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }

}
