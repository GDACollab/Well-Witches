using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Unity.Mathematics;
using System;

public class AbilityHealthTransfer : MonoBehaviour
{

    public float temp;
    public float healthgate = 3;

void Update() {


//TODO: Create Timer for Ability Health Transfer.
//TODO: Create Rounds up or down (Math.Round())

    if (Input.GetKeyDown(KeyCode.Q) && StatsManager.Instance.GathererCurrentHealth > healthgate) { //Ability confirmed to be Q && Timer
        // transfer health from Gatherer to Wanderer

      temp = StatsManager.Instance.GathererMaxHealth *  StatsManager.Instance.healthTransferAmount; // temp holds %25 percent of Gatherer's current health

      StatsManager.Instance.GathererCurrentHealth -= math.round(temp); // Subtract from current gatherer health

      //

      StatsManager.Instance.wandererHealth += math.round(temp); // Adds to current Warden health

        if(StatsManager.Instance.wandererHealth > 10) { //above 10 health
            StatsManager.Instance.wandererHealth = 10;
        }
            
            temp = 0f;

            Debug.Log("Current Warden Health: " + StatsManager.Instance.wandererHealth);
            Debug.Log("Current Gatherer Health: " + StatsManager.Instance.GathererCurrentHealth);

        }
}

}
