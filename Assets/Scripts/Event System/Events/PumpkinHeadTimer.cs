using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinHeadTimer : PumpkinHead
{
    // This script is used to manage a timer for when Gallahn Head is picked up
    // If player attacked three times after collecting said head, pumpkin destroyed, they have to find another
    // :(
    private float timer = 0f; // Timer to track time since pumpkin head was collected
    private bool isPumpkinCollected = false; // Flag to check if pumpkin head is collected
    

    public void pumpkinTimer(){
        if (isPumpkinCollected)
        {
            timer += Time.deltaTime; // Increment timer
            if (timer == 60) // Check if 60 seconds have passed
            {
                Destroy(this.gameObject); // Destroy the pumpkin head
               AnnouncementManager.Instance.AddAnnouncementToQueue("Gallahn Head have been safely recovered, head on over to Gallahn and return his head next time you see him, he won't!");
            }
        }
    }
    
}
