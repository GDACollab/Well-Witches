using System.Collections;
using UnityEngine;

public class BossShieldBash : MonoBehaviour
{
    public Transform attackPoint; // Position where the warning appears
    public float warningDuration = 1.5f; // Time before attack
    public float attackDuration = 0.3f; // How long hitbox is active
    public int damageAmount = 20;

    private SpriteRenderer warningRenderer; // Reference to warning area
    private BoxCollider2D hitboxCollider; // Collider for damage
    private BossEnemy bossEnemy;           // Reference to the BossEnemy script
    public bool IsCasting { get; private set; } = false; 
    private void Start()
    {
        // Find the warning object within the boss
        Transform warningObject = transform.Find("ShieldBashWarning");
        if (warningObject != null)
        {
            warningRenderer = warningObject.GetComponent<SpriteRenderer>();
            hitboxCollider = warningObject.GetComponent<BoxCollider2D>();
            bossEnemy = GetComponentInParent<BossEnemy>();
        }
        else
        {
            Debug.LogError("ShieldBashWarning not found! Make sure it's a child of the Boss Enemy.");
        }

        // Ensure warning is initially disabled
        if (warningRenderer != null) warningRenderer.enabled = false;
        if (hitboxCollider != null) hitboxCollider.enabled = false;
    }

    public void PerformShieldBash()
    {
        if (!IsCasting) StartCoroutine(ShieldBashRoutine());
    }

    private IEnumerator ShieldBashRoutine()
    {
        PhaseOne phaseOne = GetComponentInParent<PhaseOne>(); // Access PhaseOne to set the casting flag

        phaseOne.SetAbilityCasting(true); // Set casting flag to true

        IsCasting = true;  // The ability is now being cast

        FaceTarget(); // Face the target before attacking

        warningRenderer.color = new Color(1, 0, 0, 0.5f); // Semi-transparent red
        warningRenderer.enabled = true;
        yield return new WaitForSeconds(warningDuration);

        warningRenderer.color = new Color(1, 0, 0, 1f); // Fully visible red
        hitboxCollider.enabled = true;
        yield return new WaitForSeconds(attackDuration);

        warningRenderer.enabled = false;
        hitboxCollider.enabled = false;

        IsCasting = false;  // The ability has finished

        phaseOne.SetAbilityCasting(false); // Reset casting flag to false
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hitboxCollider.enabled && collision.CompareTag("Player"))
        {
            // Apply damage if player is inside hitbox
            Debug.Log("Player hit by Shield Bash!");
        }
    }
    private void FaceTarget()
    {
        if (bossEnemy.currentTarget != null)
        {
            // Get the direction vector to the target
            Vector2 directionToTarget = (bossEnemy.currentTarget.position - transform.position).normalized;

            // Calculate the angle to rotate
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // Apply the rotation to the hitbox (assuming it's a child of the boss)
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
