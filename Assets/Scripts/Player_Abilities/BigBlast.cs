using System.Collections;
using UnityEngine;

public class BigBlast : MonoBehaviour
{
	public void Activate(float lifespan)
	{
		StartCoroutine(DeathTimer(lifespan));
	}

	IEnumerator DeathTimer(float lifespan)
	{
		yield return new WaitForSeconds(lifespan);
		Destroy(gameObject);
	}

	void Update()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mouseDirection = (mousePosition - transform.position).normalized;
		transform.right = mouseDirection;
	}
}
