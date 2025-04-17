using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float warningDuration = 2f;  // Duration of warning
    public float attackDuration = 1f;   // Duration of the attack
    private bool isCasting = false;
    private SpriteRenderer warningRenderer; // Reference to the warning sprite
    private Collider2D hitboxCollider;     // Reference to the half-circle hitbox collider
    private BossEnemy bossEnemy;           // Reference to the BossEnemy script
    private AttackIndicatorCapsule attackIndicatorCapsule; // Reference to the AttackIndicatorSquare script
    private SpriteRenderer InnerGrow;

    private void Start()
    {
        Transform warningObject = transform.Find("SwordSlashWarning");
        if (warningObject != null)
        {
            warningRenderer = warningObject.Find("ShapeAndColliders").GetComponent<SpriteRenderer>();
            hitboxCollider = warningObject.Find("ShapeAndColliders").GetComponent<Collider2D>();
            InnerGrow = warningObject.Find("InnerGrow").GetComponent<SpriteRenderer>();
            attackIndicatorCapsule = warningObject.GetComponent<AttackIndicatorCapsule>();

            Debug.Log("Initialization successful");
            Debug.Log("warningRenderer: " + (warningRenderer != null));
            Debug.Log("hitboxCollider: " + (hitboxCollider != null));
            Debug.Log("InnerGrow: " + (InnerGrow != null));
            Debug.Log("attackIndicatorSquare: " + (attackIndicatorCapsule != null));
        }
        else
        {
            Debug.LogError("SwordSlashWarning not found! Make sure it's a child of the Boss Enemy.");
        }
        bossEnemy = GetComponentInParent<BossEnemy>();
    }

    public void PerformSwordAttack()
    {
        if (!isCasting)
        {
            StartCoroutine(SwordAttackRoutine());
        }
    }

    private IEnumerator SwordAttackRoutine()
    {

        PhaseOne phaseOne = GetComponentInParent<PhaseOne>(); // Access PhaseOne to set the casting flag
        phaseOne.SetAbilityCasting(true); // Set casting flag to true

        // Set the flag for casting
        isCasting = true;

        // Enable visibility of the warning renderer and InnerGrow
        if (warningRenderer != null) warningRenderer.enabled = true;
        if (InnerGrow != null) InnerGrow.enabled = true;

        // Gradually increase the size of the attack indicator
        float elapsedTime = 0f;
        while (elapsedTime < warningDuration)
        {
            if (attackIndicatorCapsule != null)
            {
                attackIndicatorCapsule.size = Mathf.Lerp(0, 1, elapsedTime / warningDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (attackIndicatorCapsule != null) attackIndicatorCapsule.size = 1f;

        // Check if the player is within the hitbox
        if (bossEnemy != null && bossEnemy.currentTarget != null)
        {
            Collider2D playerCollider = bossEnemy.currentTarget.GetComponent<Collider2D>();
            if (hitboxCollider != null && playerCollider != null && hitboxCollider.bounds.Intersects(playerCollider.bounds))
            {
                // Player is within the hitbox, apply damage here
                // bossEnemy.currentTarget.GetComponent<Player>().TakeDamage(damage);
            }
        }

        // Wait for the attack duration
        yield return new WaitForSeconds(attackDuration);

        // Disable visibility of the warning renderer and InnerGrow
        if (warningRenderer != null) warningRenderer.enabled = false;
        if (InnerGrow != null) InnerGrow.enabled = false;

        // Reset casting state
        isCasting = false;

        phaseOne.SetAbilityCasting(false); // Reset casting flag to false

    }
}