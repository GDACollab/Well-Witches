using System.Collections.Generic;
using UnityEngine;

public class Gatherer_Interact : MonoBehaviour
{
	[SerializeField] Collider2D interactionRadius;

	void OnInteract()   // called by the Player Input component
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
}
