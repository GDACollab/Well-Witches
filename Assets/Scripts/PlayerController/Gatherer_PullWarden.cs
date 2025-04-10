using UnityEngine;

public class Gatherer_PullWarden : MonoBehaviour
{
	[Header("Pull")]
	[SerializeField, Tooltip("Amount of force when Warden is at max tether distance")] float maxPullForce;
	[SerializeField] float pullCooldown;
	Rigidbody2D rb_Warden;
	float pullCounter = 0f;

	[Header("References")]
	[SerializeField] GameObject warden;
	[SerializeField] CircleCollider2D ropeRadius;

	void Awake()
	{
		rb_Warden = warden.GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (pullCounter > 0) pullCounter -= Time.deltaTime;
	}

	void OnPullWarden() // called by the Player Input component
	{
		if (pullCounter > 0) return;    // on cooldown

		rb_Warden.velocity = Vector2.zero;

		Vector2 direction = (Vector2)(transform.position - warden.transform.position);
		float ratio = direction.magnitude / ropeRadius.radius;
		float force = Mathf.Lerp(0f, maxPullForce, ratio);  // pull harder the further Warden is from Gatherer

		rb_Warden.AddForce(direction * force, ForceMode2D.Impulse);

		pullCounter = pullCooldown;
	}
}
