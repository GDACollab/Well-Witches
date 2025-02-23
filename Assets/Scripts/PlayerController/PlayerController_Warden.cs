using UnityEngine;

public class PlayerController_Warden : PlayerController
{
	[Header("Rope Controls")]
	[SerializeField] SpringJoint2D joint;
	LineRenderer ropeLR;
	[Tooltip("Set the degree to suppress spring oscillation. In the range 0 to 1, the higher the value, the less movement.")]
    [SerializeField][Range(0f, 1f)] float ropeDampening;
    [Tooltip("Changes how 'stiff' the rope is, the higher the value, the more stiff")]
    [SerializeField][Range(0.01f,10f)] float ropeStiffness;

	[Header("References")]
	[SerializeField] GameObject gatherer;
	[SerializeField] CircleCollider2D gathererRopeRadius;

	// Rope Test Variables
	Gradient gradient;
    Gradient gradientStressed;

	void OnValidate()
	{
		joint.frequency = ropeStiffness;
		joint.dampingRatio = ropeDampening;
	}

    void Start()
    {
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
    }

    void Update()
    {
        joint.connectedAnchor = gatherer.transform.position;

        // Rope visualization test
        ropeLR.SetPosition(0,transform.position);
        ropeLR.SetPosition(1, gatherer.transform.position);
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
}
