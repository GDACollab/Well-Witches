using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Gatherer : PlayerController
{
	[Header("Pull Warden")]
	[SerializeField, Tooltip("Amount of force when Warden is at max tether distance")] float maxPullForce;
	[SerializeField] float pullCooldown;
	Rigidbody2D rb_Warden;
	float pullCounter = 0f;

	[Header("Interaction")]
	[SerializeField] Collider2D interactionRadius;

	[Header("References")]
	[SerializeField] GameObject warden;
	[SerializeField] CircleCollider2D ropeRadius;

	// Called by the Player Input component
	void OnPullWarden()
	{
		if (pullCounter > 0) return;	// on cooldown

		rb_Warden.velocity = Vector2.zero;

		Vector2 direction = (Vector2)(transform.position - warden.transform.position);
		float ratio = direction.magnitude / ropeRadius.radius;
		float force = Mathf.Lerp(0f, maxPullForce, ratio);	// pull harder the further Warden is from Gatherer

		rb_Warden.AddForce(direction * force, ForceMode2D.Impulse);

		pullCounter = pullCooldown;
	}

	// Called by the Player Input component
	void OnInteract()
	{
		List<Collider2D> colliderList = new List<Collider2D>();
		ContactFilter2D contactFilter = new ContactFilter2D();
		interactionRadius.OverlapCollider(contactFilter.NoFilter(), colliderList);

		foreach (Collider2D collider in colliderList)
		{
			if (collider.gameObject.TryGetComponent(out IInteractable interactableObject))
			{
				interactableObject.Interact();
				break;
			}
		}
	}

	void Awake()
	{
		base.Awake();
		rb_Warden = warden.GetComponent<Rigidbody2D>();
	}

    void Update()
	{
		if (pullCounter > 0) pullCounter -= Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Player")) Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
    }
}
