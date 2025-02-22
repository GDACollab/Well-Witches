using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] PlayerMovementData movementData;
	Rigidbody2D rb;
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
		// Calculate direction & desired velocity
		Vector2 targetSpeed = moveDirection * maxSpeed_Adjusted;

		float accelRate = (moveDirection != Vector2.zero) ? movementData.acceleration : movementData.deceleration;

		// Conserve momentum
		if (movementData.conserveMomentum && rb.velocity.magnitude > maxSpeed_Adjusted && Vector2.Dot(rb.velocity.normalized, targetSpeed.normalized) >= 0.5) 
		{
			accelRate = 0;
			Debug.Log(Vector2.Angle(rb.velocity, targetSpeed));
        }
		Vector2 speedDiff = targetSpeed - rb.velocity;

		Vector2 movement = speedDiff * accelRate;

		rb.AddForce(movement, ForceMode2D.Force);
	}
}
