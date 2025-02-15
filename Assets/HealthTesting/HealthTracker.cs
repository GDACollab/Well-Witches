using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracking : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the Player (Blue Box)
    [SerializeField] private Transform healthBar; // Reference to the Health Bar (White Box)
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0); // Moves health bar above the player

    void Update()
    {
        if (player != null && healthBar != null)
        {
            // Set health bar's position above the player
            healthBar.position = player.position + offset;
        }
    }
}
