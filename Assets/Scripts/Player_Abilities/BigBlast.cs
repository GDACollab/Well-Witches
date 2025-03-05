using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBlast : MonoBehaviour
{
	float damagePerTick;
	float damageTickDuration;
	float damageTickCounter = 0;

	float knockbackForce;
	float knockbackTickDuration;
	float knockbackTickCounter = 0;

	HashSet<Collider2D> enemiesInBlast = new HashSet<Collider2D>();

	public void Activate(float damagePerTick, float damageTickDuration, float knockbackForce, float knockbackTickDuration, float lifespan)
	{
		this.damagePerTick = damagePerTick;
		this.damageTickDuration = damageTickDuration;
		this.knockbackForce = knockbackForce;
		this.knockbackTickDuration = knockbackTickDuration;

		StartCoroutine(DeathTimer(lifespan));
	}

	IEnumerator DeathTimer(float lifespan)
	{
		yield return new WaitForSeconds(lifespan);
		Destroy(gameObject);
	}

	void Update()
	{
		AimAtMouse();
		HandleDamageTick();
		HandleKnockbackTick();
	}

	void AimAtMouse()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mouseDirection = (mousePosition - transform.position).normalized;
		transform.right = mouseDirection;
	}

	void HandleDamageTick()
	{
		if (damageTickCounter > 0) damageTickCounter -= Time.deltaTime;
		if (damageTickCounter <= 0)
		{
			DamageEnemies();
			damageTickCounter = damageTickDuration;
		}
	}
	void DamageEnemies()
	{
		foreach (Collider2D collider in enemiesInBlast) collider.GetComponent<BaseEnemyClass>().TakeDamage(damagePerTick);
	}

	void HandleKnockbackTick()
	{
		if (knockbackTickCounter > 0) knockbackTickCounter -= Time.deltaTime;
		if (knockbackTickCounter <= 0)
		{
			KnockbackEnemies();
			knockbackTickCounter = knockbackTickDuration;
		}
	}
	void KnockbackEnemies()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mouseDirection = (mousePosition - transform.position).normalized;
		foreach (Collider2D collider in enemiesInBlast) collider.GetComponent<Rigidbody2D>().AddForce(mouseDirection * knockbackForce, ForceMode2D.Impulse);
	}

	void OnTriggerEnter2D(Collider2D collider) { if (collider.CompareTag("Enemy")) enemiesInBlast.Add(collider); }
	void OnTriggerExit2D(Collider2D collider) { enemiesInBlast.Remove(collider); } // only removes if item in set
}
