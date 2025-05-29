using FMOD.Studio;
using UnityEngine;
using UnityEngine.InputSystem;

public class Warden_Movement : PlayerMovement
{
	[Header("Rope")]
	[SerializeField, Range(0f, 1f), Tooltip("The degree to suppress spring oscillation. Higher value = less movement.")] float ropeDampening;
	[SerializeField, Range(0.01f, 10f), Tooltip("How stiff the rope is. Higher value = more stiff.")] float ropeStiffness;
	[SerializeField] SpringJoint2D joint;   // needs a reference set in the inspector so OnValidate() can work properly
	LineRenderer ropeLR;

	// Rope Test Variables
	Gradient gradient;
	Gradient gradientStressed;

	[Header("Gatherer")]
	[SerializeField] GameObject gatherer;
	[SerializeField] CircleCollider2D gathererRopeRadius;
	private EventInstance playerFootsteps;

	[Header("Stuck Detection")]
	[SerializeField] private float stuckDistanceAdded = 1.5f;
	[SerializeField] private float timeUntilStuck = 5f;
	[SerializeField] private Collider2D wardenCollider;

	private float stuckTime = 0f;

	void OnValidate()
	{
		joint.frequency = ropeStiffness;
		joint.dampingRatio = ropeDampening;
	}

	void Start()
	{
		PlayerInput input = GetComponent<PlayerInput>();
		input.actions = WardenAbilityManager.Controls.asset;
		input.defaultActionMap = "Gameplay_Warden";
		
		joint.enableCollision = true;
		joint.distance = gathererRopeRadius.radius;
		joint.anchor = Vector2.zero;
		ropeLR = GetComponent<LineRenderer>();

		// A simple 2 color gradient with a fixed alpha of 1.0f
		float alpha = 1.0f;

		gradient = new Gradient();
		gradient.SetKeys(
			new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
		);

		gradientStressed = new Gradient();
		gradientStressed.SetKeys(
			new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
		);

		playerFootsteps = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.playerFootsteps);
	}

	void Update()
	{
		joint.connectedAnchor = gatherer.transform.position;

		// Rope visualization test
		ropeLR.SetPosition(0, transform.position);
		ropeLR.SetPosition(1, gatherer.transform.position);

		UpdateSound();

		// Disable's warden collision if stuck behind something
		if (Vector3.Distance(gatherer.transform.position, transform.position) > gathererRopeRadius.radius + stuckDistanceAdded)
		{
			stuckTime += Time.deltaTime;
			if (stuckTime > timeUntilStuck)
			{
				wardenCollider.enabled = false;
			}
			else
			{
				wardenCollider.enabled = true;
			}
		}
		else
		{
			stuckTime = 0f;
			wardenCollider.enabled = true;
		}
	}

	public void enableRope()
	{
		joint.enabled = true;
		ropeLR.colorGradient = gradientStressed;
	}
	public void disableRope()
	{
		joint.enabled = false;
		ropeLR.colorGradient = gradient;
	}
	/// <summary>
	/// Public API function for powerups to call in order to update warden's settings incase any of them have been changed
	/// - Currently only updates the joint distance incase its been changed by a powerup
	/// </summary>
	public void UpdateWarden()
	{
		joint.distance = gathererRopeRadius.radius;
	}
	private void UpdateSound()
	{
		//Debug.Log(rb.velocity);
		PLAYBACK_STATE playbackState;
		playerFootsteps.getPlaybackState(out playbackState);

		if (this.canMove == false)
		{
			if (canMove == false && playbackState.Equals(PLAYBACK_STATE.PLAYING))
			{
				//print("off");
				playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
				return;
			}
		}

		if (Mathf.Abs(rb.velocity.x) > 1 || Mathf.Abs(rb.velocity.y) > 1)
		{
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
