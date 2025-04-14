using UnityEngine;

public class BubbleShield : GathererBaseAbilities
{
    private bool isShieldActive = false;
    private float shieldTimer = 0f;

    private void Start()
    {

    }

    private void Update()
    {
        // DEBUG: ACTIVATE ABILITY USING B KEY
        if (Input.GetKeyDown(KeyCode.B))
        {
            useAbility();
        }

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
        shieldTimer = durationOfAbility;
        Debug.Log($"{abilityName} activated for {durationOfAbility} seconds.");
        // Add visual/audio effect or shield logic here
    }

    private void DeactivateShield()
    {
        isShieldActive = false;
        Debug.Log($"{abilityName} deactivated.");
        // Remove shield effect logic here
    }
}
