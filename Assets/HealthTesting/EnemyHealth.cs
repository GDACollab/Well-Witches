using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Add as many player attacks and their damage as needed
[System.Serializable]
public class AttackDamagePair
{
    public GameObject attack;
    public float damage;
}

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;
    [SerializeField] private float maxHealthWidth = 100f; // Max width of health bar
    [SerializeField] private List<AttackDamagePair> attacksAndDamage;
    [SerializeField] private GameObject enemy; // The enemy itself

    private Dictionary<GameObject, float> attackDamageDictionary;

    void Start()
    {
        attackDamageDictionary = new Dictionary<GameObject, float>();

        foreach (AttackDamagePair pair in attacksAndDamage)
        {
            if (pair.attack != null) { attackDamageDictionary[pair.attack] = pair.damage; }
        }

        if (healthBar != null)
        {
            // **Initialize health bar to full width**
            healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxHealthWidth);

            // **Ensure Left starts at 0 and Right starts at 0**
            RectTransform rectTransform = healthBar.GetComponent<RectTransform>();
            rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y); // Lock left side
            rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y); // Start Right at 0

            Debug.Log($"🟢 Enemy {gameObject.name} initialized with max health width: {maxHealthWidth}");
        }
        else { Debug.LogError($"⚠️ Enemy {gameObject.name} is missing a health bar reference!"); }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        // Case 1: The enemy is hit by a PlayerAttack
        if (other.CompareTag("PlayerAttack"))  
        {
            Debug.Log($"⚔️ Enemy {gameObject.name} hit by {other.name}!");
            CheckAttackDamage(other);
        }
        else
        {
            Debug.Log($"{other.name} collided, but it's not a PlayerAttack.");
        }
    }

    void CheckAttackDamage(GameObject attack)
    {
        if (attackDamageDictionary.TryGetValue(attack, out float damage))
        {
            Debug.Log($"🔥 Enemy {gameObject.name} took {damage} damage from {attack.name}.");
            ReduceHealth(damage);
        }
        else
        {
            Debug.Log($"⚠️ {attack.name} is NOT in the damage dictionary!");
        }
    }

    void ReduceHealth(float damage)
    {
        if (healthBar == null)
        {
            Debug.LogError("Health bar reference missing!");
            return;
        }

        float currentWidth = healthBar.rect.width;
        float newWidth = Mathf.Max(currentWidth - damage, 0); // Prevent negative width

        // Get RectTransform
        RectTransform rectTransform = healthBar.GetComponent<RectTransform>();

        // **Lock Left Side**
        rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);

        // **Only Adjust Right Side**
        rectTransform.offsetMax = new Vector2(- (maxHealthWidth - newWidth), rectTransform.offsetMax.y);

        Debug.Log($"💔 Enemy {gameObject.name} health bar reduced to {newWidth}");
    }
}
