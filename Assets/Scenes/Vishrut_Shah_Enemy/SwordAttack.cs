using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float warningDuration = 2f;  // Duration of warning
    public float attackDuration = 1f;   // Duration of the attack
    private bool isCasting = false;
    private bool playerInHitbox = false; // Flag to track if the player is in the hitbox
    private SpriteRenderer warningRenderer; // Reference to the warning sprite
    private PolygonCollider2D hitboxCollider;     // Reference to the half-circle hitbox collider
    private BossEnemy bossEnemy;           // Reference to the BossEnemy script
    private AttackIndicatorCapsule attackIndicatorCapsule; // Reference to the AttackIndicatorSquare script
    private SpriteRenderer InnerGrow;

    public float damage;
    private bool gathererInRange = false;
    private bool wardenInRange = false;
    private void Start()
    {
        Transform warningObject = transform.Find("SwordSlashWarning");
        if (warningObject != null)
        {
            warningRenderer = warningObject.Find("ShapeAndColliders").GetComponent<SpriteRenderer>();
            hitboxCollider = warningObject.Find("ShapeAndColliders").GetComponent<PolygonCollider2D>();
            InnerGrow = warningObject.Find("InnerGrow").GetComponent<SpriteRenderer>();
            attackIndicatorCapsule = warningObject.GetComponent<AttackIndicatorCapsule>();

        }
        else
        {
            Debug.LogError("SwordSlashWarning not found! Make sure it's a child of the Boss Enemy.");
        }
        bossEnemy = GetComponentInParent<BossEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Keeps track if gatherer is in hitbox
            if (other.gameObject.name == "Gatherer")
            {
                gathererInRange = true;
            }
            else if (other.gameObject.name == "Warden")
            {
                wardenInRange = true;
            }
            playerInHitbox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Keeps track if gatherer is in hitbox
            if (other.gameObject.name == "Gatherer")
            {
                gathererInRange = false;
            }
            else if (other.gameObject.name == "Warden")
            {
                wardenInRange = false;
            }
            playerInHitbox = false;
        }
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
        yield return null;

        // Set the flag for casting
        isCasting = true;

        // Enable visibility of the warning renderer and InnerGrow
        if (warningRenderer != null) warningRenderer.enabled = true;
        if (InnerGrow != null) InnerGrow.enabled = true;

        // Calculate the direction to the player
        if (bossEnemy != null && bossEnemy.currentTarget != null)
        {
            Vector3 directionToPlayer = (bossEnemy.currentTarget.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            // Set the position and rotation of the attack indicator
            if (attackIndicatorCapsule != null)
            {
                attackIndicatorCapsule.transform.position = transform.position + directionToPlayer * 2;
                attackIndicatorCapsule.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        // Start slash animation (currently not adjusted based on slash duration TODO)
        bossEnemy.animator.SetTrigger("DoSlash");

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

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.bossPhase1Attack2, this.transform.position);
        // Play sword slash animation here


        // Check if the player is within the hitbox
        if (playerInHitbox)
        {
            if (gathererInRange)
            {
                EventManager.instance.playerEvents.PlayerDamage(damage, "Gatherer");
            }
            if (wardenInRange)
            {
                EventManager.instance.playerEvents.PlayerDamage(damage, "Warden");

            }
            Debug.Log("Player hit by sword attack!");
        }
        else
        {
            Debug.Log("Player not hit by sword attack.");
        }

        // Wait for the attack duration
        yield return new WaitForSeconds(attackDuration);

        // Disable visibility of the warning renderer and InnerGrow
        if (warningRenderer != null) warningRenderer.enabled = false;
        if (InnerGrow != null) InnerGrow.enabled = false;

        // Reset casting state
        isCasting = false;
    }
}