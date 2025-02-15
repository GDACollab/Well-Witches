using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;
    [SerializeField] private float maxHealthWidth = 100f;
    [SerializeField] private float damagePerHit = 20f;

    void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxHealthWidth);
            Debug.Log($"🟢 Enemy {gameObject.name} initialized with max health width: {maxHealthWidth}");
        }
        else
        {
            Debug.LogError($"⚠️ Enemy {gameObject.name} is missing a health bar reference!");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            Debug.Log($"⚔️ Enemy {gameObject.name} hit by {collision.gameObject.name}! Taking {damagePerHit} damage.");
            ReduceHealth(damagePerHit);
        }
    }

    void ReduceHealth(float damage)
    {
        if (healthBar == null)
        {
            Debug.LogError($"⚠️ Enemy {gameObject.name} has no health bar assigned!");
            return;
        }

        float currentWidth = healthBar.rect.width;
        float newWidth = Mathf.Max(currentWidth - damage, 0); // Prevent negative width

        Debug.Log($"💔 Enemy {gameObject.name} health bar shrinking! New width: {newWidth}");

        // Get RectTransform
        RectTransform rectTransform = healthBar.GetComponent<RectTransform>();

        // **Lock Left Side**
        rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);

        // **Only Adjust Right Side**
        rectTransform.offsetMax = new Vector2(- (maxHealthWidth - newWidth), rectTransform.offsetMax.y);
    }
}
