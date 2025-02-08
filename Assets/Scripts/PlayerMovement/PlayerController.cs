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
		Vector2 targetSpeed = moveDirection * maxSpeed_Adjusted;

		float accelRate = (Mathf.Abs(targetSpeed.x) > 0.01f && Mathf.Abs(targetSpeed.y) > 0.01f) ? movementData.accelForce : movementData.decelForce;

		// Conserve momentumn
		if (movementData.conserveMomentum && rb.velocity.magnitude > targetSpeed.magnitude && Vector2.Dot(rb.velocity.normalized, targetSpeed.normalized) == 1)
		{
			accelRate = 0;
		}
		Vector2 speedDiff = targetSpeed - rb.velocity;

		Vector2 movement = speedDiff * accelRate;

		rb.AddForce(movement, ForceMode2D.Force);
	}
}
