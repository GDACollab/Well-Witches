using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ObstacleDamagePair
{
    public GameObject obstacle;
    public float damage;
}

public class Health : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private List<ObstacleDamagePair> obstaclesAndDamage;
    [SerializeField] private GameObject player;
   
    private float currentHealth;
    private float initialHealthBarWidth;
    private Dictionary<GameObject, float> damageDictionary;

    void Start()
    {
        damageDictionary = new Dictionary<GameObject, float>();
        foreach (ObstacleDamagePair pair in obstaclesAndDamage)
        {
            if (pair.obstacle != null) { damageDictionary[pair.obstacle] = pair.damage; }
        }

         // Fetch health from GathererStatManagement
        if (GathererStatManagement.Instance != null)
        {
            maxHealth = GathererStatManagement.Instance.GetHealth();
        }
        else
        {
            Debug.LogError("❌ GathererStatManagement Instance is missing!");
            maxHealth = 100f; // Fallback value
        }
        currentHealth = maxHealth;
        UpdateHealthBar();

        // Store the initial width of the health bar at runtime
        if (healthBar != null) { initialHealthBarWidth = healthBar.rect.width; }
        else { Debug.LogError("Health bar reference missing!"); }
        UpdateHealthBar();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        // Player collides with an obstacle
        if (collision.gameObject.CompareTag("Obstacle") && damageDictionary.ContainsKey(other))
        {
            CheckObstacleDamage(other);
        }
    }

    void CheckObstacleDamage(GameObject obstacle)
    {
        if (damageDictionary.TryGetValue(obstacle, out float damage))
        {
            Debug.Log($"Player hit {obstacle.name} and took {damage} damage.");
            ReduceHealth(damage);
        }
        else
        {
            Debug.Log($"⚠️ {obstacle.name} is NOT in damage dictionary!");
        }
    }

    void ReduceHealth(float damage)
    {
        if (healthBar == null)
        {
            Debug.LogError("Health bar reference missing!");
            return;
        }

        // Decrease health, but not below 0
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBar == null) return;

        // Get initial full width dynamically
        float fullWidth = initialHealthBarWidth; 


        // Ensure health percentage is within valid range
        float healthPercentage = Mathf.Clamp(currentHealth / maxHealth, 0f, 1f);

        // Calculate new width proportionally based on initial width
        float newWidth = Mathf.Max(fullWidth * healthPercentage, 1); // Prevent disappearing bar

        // Apply new width
        healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);

        // Lock Left Side
        // Adjust Right Side
        RectTransform rectTransform = healthBar.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);
        rectTransform.offsetMax = new Vector2(-(initialHealthBarWidth - newWidth), rectTransform.offsetMax.y);

        Debug.Log($"Health bar updated: {currentHealth}/{maxHealth} | Actual Width: {initialHealthBarWidth} -> {newWidth}");
    }
}
