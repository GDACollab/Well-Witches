using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleShield : GathererBaseAbilities
{
	public bool isShieldActive = false;
	private float shieldTimer = 0f;

    public override float duration => 10;
    public override string abilityName => "BubbleShield";

    private SpriteRenderer bubbleShieldRenderer;

    public static BubbleShield Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    void Awake()
    {
        InitSingleton();
    }

    private void Start()
	{
		// Find the sprite for bubble shield
		Transform child = transform.Find("Bubble Shield");
		if (child != null)
		{
			bubbleShieldRenderer = child.GetComponent<SpriteRenderer>();
		}
		else
		{
			Debug.LogWarning("Bubble Shield sprite not found!");
		}

		bubbleShieldRenderer.enabled = false;
	}

	private void Update()
	{
	//DEBUG: ACTIVATE ABILITY USING B KEY
		//if (Input.GetKeyDown(KeyCode.B))
		//{
		//	useAbility();
		//}

		if (isShieldActive)
		{
			shieldTimer -= Time.deltaTime;
			if (shieldTimer <= 0f)
			{
				DeactivateShield();
			}

			/*
			* // On collision
			* {
			*     // Check for interaction on enemy projectile layer
			*     {
			*         // Get projectile
			*         // Invert projectile velocity
			*     }
			*     // Check for interaction on enemy melee(?) layer
			*     {
			*         // pop (after attack process is over)
			*     }
			*     // Check for interaction with Warden
			*     {
			*         // pop
			*     }
			* }
			*/
		}
	}

	public override void useAbility()
	{
		if (!isShieldActive)
		{
			ActivateShield();
		}
	}

	private void ActivateShield()
	{
		isShieldActive = true;
		shieldTimer = duration;
		Debug.Log($"{abilityName} activated for {duration} seconds.");
		// Add visual/audio effect or shield logic here
		bubbleShieldRenderer.enabled = true;
	}

	private void DeactivateShield()
	{
		isShieldActive = false;
		Debug.Log($"{abilityName} deactivated.");
		// Remove shield effect logic here
		bubbleShieldRenderer.enabled = false;
	}
}
