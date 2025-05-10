using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] public PlayerMovementData movementData;
	protected Rigidbody2D rb;
	Vector2 moveDirection;
	[HideInInspector] public float maxSpeed_Adjusted;   // this has to exist for now because of SpeedBuff.cs
	private EventInstance playerFootsteps;
	public bool canMove = true; //boolean that enables/disables movement, used for when you harvest from bushes
	public bool isMoving = false;
	public float originalAcc;
	void OnMove(InputValue iv)  // Called by the Player Input component
	{
		//print("blah");
		moveDirection = iv.Get<Vector2>() * (canMove ? 1 : 0);
		isMoving = moveDirection != Vector2.zero;
	}

	protected void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
        maxSpeed_Adjusted = movementData.maxSpeed;
		originalAcc = movementData.acceleration;
	}

    private void Start()
    {
		playerFootsteps = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.playerFootsteps);
    }

    void FixedUpdate()
	{
		if (GathererAbilityManager.Instance.passiveAbilityName != "ZoneMomentum") {
			movementData.acceleration = originalAcc;
			maxSpeed_Adjusted = movementData.maxSpeed;
		}
		// Get the direction we need to go in order to get to where we want to go (deltaVelocity)
		Vector2 currentVelocity = rb.velocity;
		Vector2 targetVelocity = moveDirection * maxSpeed_Adjusted;
		Vector2 deltaVelocity = targetVelocity - currentVelocity;

		float acceleration;
		if (movementData.conserveMomentum && currentVelocity.magnitude > maxSpeed_Adjusted && Vector2.Dot(currentVelocity.normalized, targetVelocity.normalized) >= 0.5f) acceleration = 0;
		else if (moveDirection != Vector2.zero) acceleration = movementData.acceleration;
		else acceleration = movementData.deceleration;

		Vector2 accelerationVector = deltaVelocity * acceleration;
		rb.AddForce(accelerationVector);

		UpdateSound();
    }

	private void UpdateSound()
	{
		//Debug.Log(rb.velocity);

		if (Mathf.Abs(rb.velocity.x) > 2.5f || Mathf.Abs(rb.velocity.y) > 2.5f)
		{
			PLAYBACK_STATE playbackState;
			playerFootsteps.getPlaybackState(out playbackState);

			if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
			{
				playerFootsteps.start();
			}
		}
		else
		{
			playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
		}
	}
}
