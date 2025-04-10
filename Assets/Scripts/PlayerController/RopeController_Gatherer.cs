using UnityEngine;

public class RopeController_Gatherer : MonoBehaviour
{
	Warden_Movement pcw;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (!pcw) pcw = collision.gameObject.GetComponent<Warden_Movement>();	// only set 1st time
			pcw.disableRope();
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (!pcw) pcw = collision.gameObject.GetComponent<Warden_Movement>();   // only set 1st time
			pcw.enableRope();
		}
	}
}
