using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] PlayerMovementData movementData;
	protected Rigidbody2D rb;
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
		// Get the direction we need to go in order to get to where we want to go (deltaVelocity)
		Vector2 currentVelocity = rb.velocity;
		Vector2 targetVelocity = moveDirection * maxSpeed_Adjusted;
		Vector2 deltaVelocity = targetVelocity - currentVelocity;

		float acceleration;
		if (movementData.conserveMomentum && currentVelocity.magnitude > maxSpeed_Adjusted && Vector2.Angle(currentVelocity, targetVelocity) <= 60) acceleration = 0;
		else if (moveDirection != Vector2.zero) acceleration = movementData.acceleration;
		else acceleration = movementData.deceleration;

		Vector2 accelerationVector = deltaVelocity * acceleration;
		rb.AddForce(accelerationVector);
	}
}
