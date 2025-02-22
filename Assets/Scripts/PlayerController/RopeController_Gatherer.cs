using UnityEngine;

public class RopeController_Gatherer : MonoBehaviour
{
	PlayerController_Warden pcw;
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (!pcw) pcw = collision.gameObject.GetComponent<PlayerController_Warden>();	// only set 1st time
			pcw.disableRope();
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (!pcw) pcw = collision.gameObject.GetComponent<PlayerController_Warden>();   // only set 1st time
		pcw.enableRope();
	}
}
