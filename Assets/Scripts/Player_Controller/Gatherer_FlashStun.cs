using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gatherer_FlashStun : MonoBehaviour
{
	[SerializeField, Tooltip("Amount of time needed to get the ability off")] float chargeDuration;
	[SerializeField, Tooltip("Radius of the circle that stuns enemies inside")] float radius = 16;	// 16 measured by me (Justin L) to go from left to right side of screen
	[SerializeField] float stunDuration;
	[SerializeField] float cooldownDuration;
	[SerializeField] LayerMask collisionLayersToCheck;
	InputAction activateAbilityAction;
	float chargeCounter;
	float cooldownCounter = 0;

	void Awake()
	{
		activateAbilityAction = GetComponent<PlayerInput>().actions["Activate Ability"];
		chargeCounter = chargeDuration;
	}
	void Update()
	{
		if (cooldownCounter > 0)
		{
			cooldownCounter -= Time.deltaTime;
			return;	// don't even think about charging up if on cooldown
		}

		if (activateAbilityAction.IsPressed())
		{
			chargeCounter -= Time.deltaTime;
			if (chargeCounter <= 0)
			{
				ExecuteAbility();
				chargeCounter = chargeDuration;
				cooldownCounter = cooldownDuration;
			}
		}
		else chargeCounter = chargeDuration;
	}

	void ExecuteAbility()
	{
		Debug.Log("ACTIVATE");
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, collisionLayersToCheck);
		foreach (Collider2D collider in colliders) if (collider.CompareTag("Enemy")) { /* stun enemy */ }
	}
}
