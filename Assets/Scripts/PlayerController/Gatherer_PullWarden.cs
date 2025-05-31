using System.Collections;
using UnityEngine;

public class Gatherer_PullWarden : MonoBehaviour
{
	[Header("Pull")]
	//[SerializeField, Tooltip("Amount of force when Warden is at max tether distance")] float maxPullForce; //Handled by StatsManager now
	[SerializeField] float pullCooldown;
	Rigidbody2D rb_Warden;
    Warden_Movement wardenMovement;
	Warden_Health wardenHealth;
    float pullCounter = 0f;

	[Header("References")]
	[SerializeField] GameObject warden;
	[SerializeField] CircleCollider2D ropeRadius;

	void Awake()
	{
		rb_Warden = warden.GetComponent<Rigidbody2D>();
		wardenMovement = warden.GetComponent<Warden_Movement>();
		wardenHealth = warden.GetComponent<Warden_Health>();
	}

	void Update()
	{
		if (pullCounter > 0) pullCounter -= Time.deltaTime;
	}

	void OnPullWarden() // called by the Player Input component
	{
		if (pullCounter > 0) return;    // on cooldown
		wardenMovement.canMove = true;
		rb_Warden.velocity = Vector2.zero;

		Vector2 direction = (Vector2)(transform.position - warden.transform.position);
		float ratio = direction.magnitude / ropeRadius.radius;
		float force = Mathf.Lerp(0f, StatsManager.Instance.getYank(), ratio);  // pull harder the further Warden is from Gatherer

		rb_Warden.AddForce(direction * force, ForceMode2D.Impulse);
		AudioManager.Instance.PlayOneShot(FMODEvents.Instance.flamingPumpkinYank, this.transform.position);

		pullCounter = pullCooldown;
        if (wardenHealth.gourdForgeInvulnerability) StartCoroutine(IfForge());
    }

	IEnumerator IfForge()
	{
		yield return new WaitForSeconds(0.5f);
		wardenMovement.canMove = false;
	}
}
