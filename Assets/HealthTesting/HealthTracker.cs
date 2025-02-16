using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracking : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform healthBar;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);

    void Update()
    {
        if (player != null && healthBar != null)
        {
            // Preserve X 
            // Adjust Y to follow the player
            Vector3 fixedX = new Vector3(healthBar.position.x, player.position.y + offset.y, healthBar.position.z);
            healthBar.position = fixedX;
        }
    }

}
