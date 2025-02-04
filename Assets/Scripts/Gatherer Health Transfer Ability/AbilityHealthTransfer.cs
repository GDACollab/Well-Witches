using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHealthTransfer : MonoBehaviour
{

void Update() {

    if (Input.GetKeyDown(KeyCode.Q)) { //Ability confirmed to be Q
        // transfer health from Gatherer to Wanderer
        StatsManager.Instance.GathererCurrentHealth-= StatsManager.Instance.healthTransferAmount;


        StatsManager.Instance.wandererHealth+= StatsManager.Instance.healthTransferAmount;

        if(StatsManager.Instance.wandererHealth > 10) { //above 10 health
            StatsManager.Instance.wandererHealth = 10;
        }
            Debug.Log("wander health:" + StatsManager.Instance.wandererHealth);
            Debug.Log("wander health:" + StatsManager.Instance.wandererHealth);

    }
}

}
