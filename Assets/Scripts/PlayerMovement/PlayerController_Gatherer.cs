using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_Gatherer : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] PlayerMovementData movementData;
    Rigidbody2D rb;
    Vector2 moveDirection;

	[Header("Interaction")]
	[SerializeField] Collider2D interactRangeCollider;

	private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
		interactRangeCollider = GetComponent<Collider2D>();
	}

    // Called by the Player Input component
	void OnMove(InputValue inputValue)
	{
		moveDirection = inputValue.Get<Vector2>();
	}

	// Called by the Player Input component
	void OnPickUpItem()
	{
		List<Collider2D> colliderList = new List<Collider2D>();
		ContactFilter2D contactFilter = new ContactFilter2D();
		interactRangeCollider.OverlapCollider(contactFilter.NoFilter(), colliderList);

		foreach (Collider2D collider in colliderList)
		{
			if (collider.gameObject.TryGetComponent(out IInteractable interactableObject))
			{
				interactableObject.Interact();
				break;
			}
		}
	}

	void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Calculate direction & desired velocity
        Vector2 targetSpeed = moveDirection * movementData.maxSpeed;

        float accelRate = (Mathf.Abs(targetSpeed.x) > 0.01f && Mathf.Abs(targetSpeed.y) > 0.01f) ? movementData.accelerationForce : movementData.decelerationForce;

        // Conserve momentumn
        if (movementData.conserveMomentum && rb.velocity.magnitude > targetSpeed.magnitude && Vector2.Dot(rb.velocity.normalized, targetSpeed.normalized) == 1)
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
