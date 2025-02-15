using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Add as many obstacles and damages as you want
[System.Serializable]
public class ObstacleDamagePair
{
    public GameObject obstacle;
    public float damage;
}

public class Health : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar;
    //Adjust Health bar width according to stats
    [SerializeField] private float maxHealthWidth = 100f;
    [SerializeField] private List<ObstacleDamagePair> obstaclesAndDamage;
    [SerializeField] private GameObject player;

    private Dictionary<GameObject, float> damageDictionary;

    void Start()
    {
        damageDictionary = new Dictionary<GameObject, float>();
        foreach (ObstacleDamagePair pair in obstaclesAndDamage)
        {
            if (pair.obstacle != null) { damageDictionary[pair.obstacle] = pair.damage; }
        }

        if (healthBar != null) { healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxHealthWidth); }
        else { Debug.LogError(" Health bar reference missing!"); }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        // Case 1: The Player collides with an obstacle
        if (other == player)  
        {
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
        else { Debug.Log($"{other.name} collided, but it's not the player or a recognized obstacle."); }
    }

    void CheckObstacleDamage(GameObject obstacle){
        if (damageDictionary.TryGetValue(obstacle, out float damage))
        {
            Debug.Log($"💥 Player hit {obstacle.name} and took {damage} damage.");
            ReduceHealth(damage);
        }
        else { Debug.Log($"⚠️ {obstacle.name} is NOT in damage dictionary!"); }
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
    }
}

