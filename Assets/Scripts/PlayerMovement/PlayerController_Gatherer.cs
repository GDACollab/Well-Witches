using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Gatherer : PlayerController
{
	[Header("Interaction")]
	[SerializeField] Collider2D interactRangeCollider;

	void Awake()
    {
        base.Awake();
		interactRangeCollider = GetComponent<Collider2D>();
	}

	// Called by the Player Input component
	void OnPickUpItem()
	{
		List<Collider2D> colliderList = new List<Collider2D>();
		ContactFilter2D contactFilter = new ContactFilter2D();
		interactRangeCollider.OverlapCollider(contactFilter.NoFilter(), colliderList);

		foreach (Collider2D collider in colliderList)
		{
			if (collider.gameObject.TryGetComponent(out IInteractable interactableObject))
			{
				interactableObject.Interact();
				break;
			}
		}
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController_Warden warden = collision.gameObject.GetComponent<PlayerController_Warden>();
            warden.disableRope();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController_Warden warden = collision.gameObject.GetComponent<PlayerController_Warden>();
            warden.enableRope();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }

}
