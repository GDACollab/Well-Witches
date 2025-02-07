using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_Warden : MonoBehaviour
{
    [SerializeField] PlayerMovementData movementData;
	Rigidbody2D rb;
	LineRenderer ropeLR;
    Vector2 moveDirection;

	[Header("References")]
	[Tooltip("Place Gatherer here, or whatever you want warden to attatch to")]
    [SerializeField] GameObject gatherer;
    [Tooltip("Place Gatherer's CircleCollider2D here")]
    [SerializeField] CircleCollider2D gathererRadiusCircle;

	[Header("Rope Controls")]
	[SerializeField] SpringJoint2D joint;
	[Tooltip("Set the degree to suppress spring oscillation. In the range 0 to 1, the higher the value, the less movement.")]
    [SerializeField][Range(0f, 1f)] float ropeDampening;
    [Tooltip("Changes how 'stiff' the rope is, the higher the value, the more stiff")]
    [SerializeField][Range(0.01f,10f)] float ropeStiffness;

    //Rope Test Variables
    Gradient gradient;
    Gradient gradientStressed;

	void OnValidate()
	{
		joint.frequency = ropeStiffness;
		joint.dampingRatio = ropeDampening;
	}

	void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<SpringJoint2D>();
        if (!joint) Debug.Log("ERROR");
    }

    void Start()
    {
        joint.enableCollision = true;
        joint.distance = gathererRadiusCircle.radius;
        joint.anchor = Vector2.zero;
        ropeLR = GetComponent<LineRenderer>();

        // A simple 2 color gradient with a fixed alpha of 1.0f.
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
    }

    void Update()
    {
        // Calcuate spring's anchor position
        joint.connectedAnchor = gatherer.transform.position;

        // ROPE visualization test
        ropeLR.SetPosition(0,transform.position);
        ropeLR.SetPosition(1, gatherer.transform.position);

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

        // Conserve momentum, if the velocity is faster than max speed (i.e from being launched) AND the target speed is in the same direction as velocity, don't slow down the player
        if (movementData.conserveMomentum && rb.velocity.magnitude > targetSpeed.magnitude && Vector2.Dot(rb.velocity.normalized, targetSpeed.normalized) == 1)
        {
            accelRate = 0;
        }
        Vector2 speedDif = targetSpeed - rb.velocity;

        Vector2 movement = speedDif * accelRate;

        rb.AddForce(movement, ForceMode2D.Force);
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
    /// Public API function for powerups to call in order to update wanderer's settings incase any of them have been changed
    /// - Currently only updates the joint distance incase its been changed by a powerup
    /// </summary>
    public void UpdateWanderer()
    {
        joint.distance = gathererRadiusCircle.radius;
    }
}
