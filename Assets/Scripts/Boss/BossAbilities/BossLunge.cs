using System.Collections;
using UnityEngine;

public class BossLunge : MonoBehaviour
{
    public float warningDuration = 1.5f; // Time before attack
    public float attackDuration = 0.3f; // How long hitbox is active
    public float stunDuration = 2f; // Duration of stun if the attack misses
    public float LungeDistance; // The distance the Player has to be from the boss for the lunge attack to activate
    public float LungeSpeed; // Speed of the lunge attack
    public float damage; //How much damage will be dealt to players on hit

    private bool isCasting = false;
    private bool playerInHitbox = false; // Flag to track if the player is in the hitbox
    private SpriteRenderer warningRenderer; // Reference to warning area
    private BoxCollider2D hitboxCollider; // Collider for damage
    private BossEnemy bossEnemy; // Reference to the BossEnemy script
    private AttackLungeIndicator attackIndicatorLunge; // Reference to the AttackIndicatorSquare script
    private SpriteRenderer InnerGrow;
    public Collider2D bossCollider; // Reference to the boss's collider

    private void Start()
    {
        // Find the warning object within the boss
        Transform warningObject = transform.Find("LungeWarning");
        if (warningObject != null)
        {
            warningRenderer = warningObject.Find("ShapeAndColliders").GetComponent<SpriteRenderer>();
            hitboxCollider = warningObject.Find("ShapeAndColliders").GetComponent<BoxCollider2D>();
            InnerGrow = warningObject.Find("InnerGrow").GetComponent<SpriteRenderer>();
            attackIndicatorLunge = warningObject.GetComponent<AttackLungeIndicator>();
        }
        else
        {
            Debug.LogError("LungeWarning not found! Make sure it's a child of the Boss Enemy.");
        }
        bossEnemy = GetComponentInParent<BossEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInHitbox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInHitbox = false;
        }
    }

    public void PerformLunge()
    {
        if (!isCasting)
        {
            StartCoroutine(LungeRoutine());
        }
    }

    private IEnumerator LungeRoutine()
    {
        PhaseOne phaseOne = GetComponentInParent<PhaseOne>(); // Access PhaseOne to set the casting flag
        phaseOne.SetAbilityCasting(true); // Set casting flag to true

        // Set the flag for casting
        isCasting = true;

        // Enable visibility of the warning renderer and InnerGrow
        if (warningRenderer != null) warningRenderer.enabled = true;
        if (InnerGrow != null) InnerGrow.enabled = true;

        // Calculate the direction to the player
        Vector3 targetPosition = Vector3.zero;
        if (bossEnemy != null && bossEnemy.currentTarget != null)
        {
            Vector3 directionToPlayer = (bossEnemy.currentTarget.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            // Set the position and rotation of the attack indicator
            if (attackIndicatorLunge != null)
            {
                attackIndicatorLunge.transform.position = transform.position + directionToPlayer * 6;
                attackIndicatorLunge.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }

            targetPosition = bossEnemy.currentTarget.position;
        }

        // Gradually increase the size of the attack indicator
        float elapsedTime = 0f;
        while (elapsedTime < warningDuration)
        {
            if (attackIndicatorLunge != null)
            {
                attackIndicatorLunge.size = Mathf.Lerp(0, 1, elapsedTime / warningDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (attackIndicatorLunge != null) attackIndicatorLunge.size = 1f;

        // Stop following the player and dash towards the target position
        Vector3 dashDirection = (targetPosition - transform.position).normalized;
        float dashDistance = Vector3.Distance(transform.position, targetPosition);
        float dashSpeed = LungeSpeed;

        float dashTime = dashDistance / dashSpeed;
        float dashElapsedTime = 0f;

        // Disable visibility of the warning renderer and InnerGrow
        if (warningRenderer != null) warningRenderer.enabled = false;
        if (InnerGrow != null) InnerGrow.enabled = false;

        while (dashElapsedTime < dashTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, dashSpeed * Time.deltaTime);
            dashElapsedTime += Time.deltaTime;

            // Check for collision with the player during the dash
            Collider2D[] hits = Physics2D.OverlapBoxAll(bossCollider.bounds.center, bossCollider.bounds.size, 0);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    playerInHitbox = true;
                    if (hit.gameObject.name == "Gatherer")
                    {
                        //Debug.Log("Gatherher is hit");
                        EventManager.instance.playerEvents.PlayerDamage(damage, "Gatherer");
                    }
                    else if (hit.gameObject.name == "Warden")
                    {
                        EventManager.instance.playerEvents.PlayerDamage(damage, "Warden");

                    }
                    break;
                }
            }

            yield return null;
        }

        // Check if the player is within the hitbox
        if (playerInHitbox)
        {
            Debug.Log("Player hit by lunge attack!");

        }
        else
        {
            Debug.Log("Player not hit by lunge attack. Boss stunned.");
            yield return new WaitForSeconds(stunDuration);
        }

        // Wait for the attack duration
        yield return new WaitForSeconds(attackDuration);

        // Reset casting state
        isCasting = false;

        phaseOne.SetAbilityCasting(false); // Reset casting flag to false
    }
}