using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] PlayerMovementData movementData;
	public Rigidbody2D rb;
	Vector2 moveDirection;
	[HideInInspector] public float maxSpeed_Adjusted;	// this has to exist for now because of SpeedBuff.cs

	protected void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		maxSpeed_Adjusted = movementData.maxSpeed;
	}

	// Called by the Player Input component
	void OnMove(InputValue iv)
	{
		moveDirection = iv.Get<Vector2>();
	}

    void FixedUpdate()
	{
		Move();
	}

	void Move()
	{
		Vector2 currentVelocity = rb.velocity;
		Vector2 targetVelocity = moveDirection * maxSpeed_Adjusted;
		Vector2 deltaVelocity = targetVelocity - currentVelocity;

		float acceleration;
		if (movementData.conserveMomentum && currentVelocity.magnitude > maxSpeed_Adjusted && Vector2.Dot(rb.velocity.normalized, targetVelocity.normalized) >= 0.5) 
		{
			acceleration = 0;
        }
		else acceleration = (moveDirection != Vector2.zero) ? movementData.acceleration : movementData.deceleration;

		Vector2 accelerationVector = deltaVelocity * acceleration;

		rb.AddForce(accelerationVector, ForceMode2D.Force);
	}
}
