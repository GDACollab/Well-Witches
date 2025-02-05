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
   public float cooldownTime = 5f; //creates the five second cooldown
   private float lastUsedTime;
   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Q) && Time.time > lastUsedTime + cooldownTime)
      { //varifies that someone isn't spamming the q button and there is a gap between presses (Abiltiy Cooldown)
         if (Input.GetKeyDown(KeyCode.Q) && StatsManager.Instance.GathererCurrentHealth > healthgate)
         { //Ability confirmed to be Q && Timer
           // transfer health from Gatherer to Wanderer
            lastUsedTime = Time.time;

            temp = StatsManager.Instance.GathererMaxHealth * StatsManager.Instance.healthTransferAmount; // temp holds %25 percent of Gatherer's current health

            StatsManager.Instance.GathererCurrentHealth -= math.round(temp); // Subtract from current gatherer health



            StatsManager.Instance.wandererHealth += math.round(temp);

            if (StatsManager.Instance.wandererHealth > 10)
            { //above 10 health
               StatsManager.Instance.wandererHealth = 10;
            }

            Debug.Log("Health of Gatherer:" + StatsManager.Instance.GathererCurrentHealth);
            Debug.Log("Health of Warden:" + StatsManager.Instance.wandererHealth);
            temp = 0f;
         }


      }
   }

}