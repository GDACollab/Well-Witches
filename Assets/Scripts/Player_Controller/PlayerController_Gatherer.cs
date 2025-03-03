using UnityEngine;

public class PlayerController_Gatherer : PlayerMovement
{	
	void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Player")) Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
    }
}
