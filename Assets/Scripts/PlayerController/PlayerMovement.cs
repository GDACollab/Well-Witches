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

	public Animator animator;
	public SpriteRenderer sprite;

	//public SpriteRenderer sprite2;

	protected virtual void changeSpriteTo()
	{
		animator.SetBool("isRunning", isMoving);
	}

	void OnMove(InputValue iv)  // Called by the Player Input component
	{
		moveDirection = iv.Get<Vector2>() * (canMove ? 1 : 0);
		isMoving = moveDirection.magnitude > 0;

		if (moveDirection.x > 0)
		{
			sprite.flipX = true;
		}
		else if (moveDirection.x < 0)
		{
			sprite.flipX = false;
		}
	}

	protected void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		animator = GetComponentInChildren<Animator>();
		animator.SetTrigger("Respawn");
		maxSpeed_Adjusted = movementData.maxSpeed;
		originalAcc = movementData.acceleration;
	}

	private void Start()
	{
		PlayerInput input = GetComponent<PlayerInput>();
		input.actions = GathererAbilityManager.Controls.asset;
		input.defaultActionMap = "Gameplay_Gatherer";
		playerFootsteps = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.playerFootsteps);
	}

	void FixedUpdate()
	{
		if (GathererAbilityManager.Instance.GetEquippedPassiveName() != "Espresso")
		{
			movementData.acceleration = originalAcc;
			maxSpeed_Adjusted = movementData.maxSpeed;
		}
		// Get the direction we need to go in order to get to where we want to go (deltaVelocity)
		Vector2 currentVelocity = rb.velocity;
		Vector2 targetVelocity = moveDirection * maxSpeed_Adjusted * StatsManager.Instance.getSpeedMult();
		Vector2 deltaVelocity = targetVelocity - currentVelocity;

		float acceleration;
		if (movementData.conserveMomentum && currentVelocity.magnitude > maxSpeed_Adjusted && Vector2.Dot(currentVelocity.normalized, targetVelocity.normalized) >= 0.5f) acceleration = 0;
		else if (moveDirection != Vector2.zero) acceleration = movementData.acceleration * StatsManager.Instance.getSpeedMult();
		else acceleration = movementData.deceleration * StatsManager.Instance.getSpeedMult();

		//Debug.Log("deltaVelocity:" + deltaVelocity);
		Vector2 accelerationVector = deltaVelocity * acceleration;
		if (canMove)
			rb.AddForce(accelerationVector);
		else
			rb.velocity *= 0;

		changeSpriteTo();

		UpdateSound();
	}

	private void UpdateSound()
	{
		PLAYBACK_STATE playbackState;
		playerFootsteps.getPlaybackState(out playbackState);

		//Debug.Log(rb.velocity);
		if (Mathf.Abs(rb.velocity.x) > 2.5f || Mathf.Abs(rb.velocity.y) > 2.5f)
		{
			if (playbackState.Equals(PLAYBACK_STATE.STOPPED) && canMove)
			{
				playerFootsteps.start();

				//print("on");
			}
			//Changed animation here to run
			//changeSpriteTo("isRunning");

		}
		else
		{
			//Changing animation to idle here
			//changeSpriteTo("isIdle");
			playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
		}
	}
	
	private void Update() {
		PLAYBACK_STATE playbackState;
		playerFootsteps.getPlaybackState(out playbackState);

		if (canMove == false && playbackState.Equals(PLAYBACK_STATE.PLAYING))
		{
			//print("off");
			playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
			return;
		}
	}
}
