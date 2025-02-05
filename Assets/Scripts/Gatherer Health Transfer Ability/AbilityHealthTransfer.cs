using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHealthTransfer : MonoBehaviour
{

    public float temp;
    public float healthgate = 3;

void Update() {


//TODO: Create Timer for Ability Health Transfer.
//TODO: Create Rounds up or down

    if (Input.GetKeyDown(KeyCode.Q) && StatsManager.Instance.GathererCurrentHealth > healthgate) { //Ability confirmed to be Q && Timer
        // transfer health from Gatherer to Wanderer

      temp = StatsManager.Instance.GathererMaxHealth *  StatsManager.Instance.healthTransferAmount; // temp holds %25 percent of Gatherer's current health

      StatsManager.Instance.GathererCurrentHealth -= temp; // Subtract from current gatherer health

      //

        StatsManager.Instance.wandererHealth+= temp; 

        if(StatsManager.Instance.wandererHealth > 10) { //above 10 health
            StatsManager.Instance.wandererHealth = 10;
        }
            
            temp = 0f;

    }
}

}
