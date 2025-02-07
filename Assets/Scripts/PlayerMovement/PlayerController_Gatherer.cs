using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_Gatherer : MonoBehaviour
{
    [SerializeField] PlayerMovementData movementData;
    Rigidbody2D rb;
    Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called by the Player Input component
	void OnMove(InputValue inputValue)
	{
		moveDirection = inputValue.Get<Vector2>();
	}

	void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Calculate direction & desired velocity
        Vector2 targetSpeed = moveDirection * movementData.moveSpeed;

        float accelRate = (Mathf.Abs(targetSpeed.x) > 0.01f && Mathf.Abs(targetSpeed.y) > 0.01f) ? movementData.accelAmount : movementData.decelAmount;

        // Conserve momentumn
        if (movementData.conserveMomentum && rb.velocity.magnitude > targetSpeed.magnitude && Vector2.Dot(rb.velocity.normalized,targetSpeed.normalized) == 1)
        {
            accelRate = 0;
        }
        Vector2 speedDiff = targetSpeed - rb.velocity;

        Vector2 movement = speedDiff * accelRate;

        rb.AddForce(movement, ForceMode2D.Force);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController_Warden warden = collision.gameObject.GetComponent<PlayerController_Warden>();
            warden.disableRope();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController_Warden warden = collision.gameObject.GetComponent<PlayerController_Warden>();
            warden.enableRope();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }

}
