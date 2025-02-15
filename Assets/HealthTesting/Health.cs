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
    [SerializeField] private float maxHealthWidth = 100f;
    [SerializeField] private List<ObstacleDamagePair> obstaclesAndDamage;
    [SerializeField] private GameObject player;

    private Dictionary<GameObject, float> damageDictionary;

    void Start()
    {
        damageDictionary = new Dictionary<GameObject, float>();
        foreach (ObstacleDamagePair pair in obstaclesAndDamage)
        {
            if (pair.obstacle != null)
            {
                damageDictionary[pair.obstacle] = pair.damage;
                Debug.Log($"⚡ Obstacle {pair.obstacle.name} set to deal {pair.damage} damage.");
            }
        }

        if (healthBar != null)
        {
            healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxHealthWidth);
            Debug.Log($"✅ Initial health bar width set to {maxHealthWidth}");
        }
        else
        {
            Debug.LogError("❌ Health bar reference is missing!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
{
    GameObject other = collision.gameObject;
    Debug.Log($"🚀 2D Collision detected with: {other.name}, Tag: {other.tag}");

    // Case 1: The Player collides with an obstacle
    if (other == player)  
    {
        Debug.Log("✅ Player collided with an obstacle!");
        foreach (ContactPoint2D contact in collision.contacts)
        {
            GameObject obstacle = contact.collider.gameObject;
            CheckObstacleDamage(obstacle);
        }
    }
    // Case 2: The obstacle collides with the Player (in case event triggers on the obstacle)
    else if (collision.gameObject.CompareTag("Obstacle") && damageDictionary.ContainsKey(other))
    {
        Debug.Log($"✅ Obstacle {other.name} hit the player!");
        CheckObstacleDamage(other);
    }
    else
    {
        Debug.Log($"❌ {other.name} collided, but it's not the player or a recognized obstacle.");
    }
}


    void CheckObstacleDamage(GameObject obstacle){
    if (damageDictionary.TryGetValue(obstacle, out float damage))
    {
        Debug.Log($"💥 Player hit {obstacle.name} and took {damage} damage.");
        ReduceHealth(damage);
    }
    else
    {
        Debug.Log($"⚠️ {obstacle.name} is NOT in the damage dictionary! Check if it was added correctly.");
    }
}


    void ReduceHealth(float damage)
{
    if (healthBar == null)
    {
        Debug.LogError("❌ Cannot reduce health, health bar reference is missing!");
        return;
    }

    float currentWidth = healthBar.rect.width; // Get current width
    float newWidth = Mathf.Max(currentWidth - damage, 0); // Prevent negative width

    // Set the width while keeping the left side anchored
    healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);

    Debug.Log($"❤️ Health bar width reduced to {newWidth}");
}

}
