using System.Collections;
using UnityEngine;

public class BossShieldBash : MonoBehaviour
{
    public float warningDuration = 1.5f; // Time before attack
    public float attackDuration = 0.3f; // How long hitbox is active
    public bool isCasting { get; private set; }
    private bool playerInHitbox = false; // Flag to track if the player is in the hitbox
    private SpriteRenderer warningRenderer; // Reference to warning area
    private BoxCollider2D hitboxCollider; // Collider for damage
    private BossEnemy bossEnemy; // Reference to the BossEnemy script
    private AttackIndicatorSquare attackIndicatorSquare; // Reference to the AttackIndicatorSquare script
    private SpriteRenderer InnerGrow;

    public float damage;
    private bool gathererInRange = false;
    private bool wardenInRange = false;

    private void Start()
    {
        // Find the warning object within the boss
        Transform warningObject = transform.Find("ShieldBashWarning");
        if (warningObject != null)
        {
            warningRenderer = warningObject.Find("ShapeAndColliders").GetComponent<SpriteRenderer>();
            hitboxCollider = warningObject.Find("ShapeAndColliders").GetComponent<BoxCollider2D>();
            InnerGrow = warningObject.Find("InnerGrow").GetComponent<SpriteRenderer>();
            attackIndicatorSquare = warningObject.GetComponent<AttackIndicatorSquare>();
        }
        else
        {
            Debug.LogError("ShieldBashWarning not found! Make sure it's a child of the Boss Enemy.");
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

    public void PerformShieldBash()
    {
        if (!isCasting)
        {
            StartCoroutine(ShieldBashRoutine());
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.bossPhase1Attack1, this.transform.position);
        }
    }

    private IEnumerator ShieldBashRoutine()
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
            if (attackIndicatorSquare != null)
            {
                attackIndicatorSquare.transform.position = transform.position + directionToPlayer * 2;
                attackIndicatorSquare.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        // Start shieldbash animation(currently not adjusted based on slash duration TODO)
        bossEnemy.animator.SetTrigger("DoShieldbash");

        // Gradually increase the size of the attack indicator
        float elapsedTime = 0f;
        while (elapsedTime < warningDuration)
        {
            if (attackIndicatorSquare != null)
            {
                attackIndicatorSquare.size = Mathf.Lerp(0, 1, elapsedTime / warningDuration);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (attackIndicatorSquare != null) attackIndicatorSquare.size = 1f;

        // Play shield bash animation here



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
            Debug.Log("Player hit by shield bash!");
        }
        else
        {
            Debug.Log("Player not hit by shield bash.");
        }

        // Wait for the attack duration
        yield return new WaitForSeconds(attackDuration);

        // Disable visibility of the warning renderer and InnerGrow
        if (warningRenderer != null) warningRenderer.enabled = false;
        if (InnerGrow != null) InnerGrow.enabled = false;
        isCasting = false;
    }
}