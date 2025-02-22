using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Gatherer : PlayerController
{
	[Header("Interaction")]
	[SerializeField] Collider2D interactionRadius;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Player")) Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
    }
}
