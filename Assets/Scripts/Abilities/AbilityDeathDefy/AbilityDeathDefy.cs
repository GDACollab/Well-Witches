using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public float totalTime = 45f; // CHANGE IF VALUE CHANGES FROM 45 SECONDS

    private float currentTime; // Respresents the time left in seconds on the timer
    private bool timerOn = false; // Tracks whether or not the timer is running
    private float healthIncrementValue; // The value to increment the HP by



    // Start is called before the first frame update
    void Start()
    {
        healthIncrementValue = StatsManager.Instance.WardenMaxHealth / totalTime; // Increments the HP over time with this value
    }

    // Update is called once per frame
    void Update()
    {
        if (StatsManager.Instance.WardenCurrentHealth <= 0 && timerOn == false) // Checks to see if Warden is at 0 HP
        {
            timerOn = true; // Sets the timer event to true
            currentTime = totalTime; // Sets the timer
        }



        if (timerOn)
        {
            currentTime -= Time.deltaTime; // Decrements the timer

            // Increases Warden's HP over time
            StatsManager.Instance.WardenCurrentHealth += healthIncrementValue * Time.deltaTime;

            if (currentTime <= 0f) { // If the timer is up
                currentTime = 0f; // Sets the time to 0
                timerOn = false; // Sets the timer event to false
                StatsManager.Instance.WardenCurrentHealth = StatsManager.Instance.WardenMaxHealth; // Guarantees Warden has their max HP
            }
        }
    }
}
