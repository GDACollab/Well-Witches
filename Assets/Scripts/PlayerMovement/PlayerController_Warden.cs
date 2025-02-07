using UnityEngine;

public class PlayerController_Warden : PlayerController
{
    [Header("Shooting")]
    [SerializeField][Tooltip("(In seconds)")] float shootCooldown;
    [SerializeField] float bulletSpeed;
    [SerializeField][Tooltip("(In seconds)")] float bulletLifespan;
    [SerializeField] Transform crosshair;
	[SerializeField] GameObject bulletPrefab;
    float shootCooldownCounter;

	[Header("Gatherer")]
	[Tooltip("Place Gatherer here, or whatever you want warden to attatch to")]
    [SerializeField] GameObject gatherer;
    [Tooltip("Place Gatherer's CircleCollider2D here")]
    [SerializeField] CircleCollider2D gathererRadiusCircle;

	[Header("Rope Controls")]
	[SerializeField] SpringJoint2D joint;
	LineRenderer ropeLR;
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
        base.Awake();
        joint = GetComponent<SpringJoint2D>();
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

        DecreaseCooldownCounters();
    }

    void DecreaseCooldownCounters()
    {
        if (shootCooldownCounter > 0) shootCooldownCounter -= Time.deltaTime;
	}

	// Called by the Player Input component
	void OnShoot()
    {
        if (shootCooldownCounter > 0) return;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.SetActive(true);
        Vector2 crosshairDirection = (crosshair.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = crosshairDirection * bulletSpeed;
        Destroy(bullet, bulletLifespan);    // destroy the bullet after a some time; a horrible way to handle getting rid of bullets but this is all temporary so...

        shootCooldownCounter = shootCooldown;
	}

	// Called by the Player Input component
	void OnActiveAbility()
    {
        Debug.Log("Warden active ability activated");   // temporary, so we can see that something's actually happening
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
