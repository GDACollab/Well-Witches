using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using System;

public class AbilityHealthTransfer : GathererBaseAbilities
{

   public float temp; //hold the % of health from Singleton that holds that %value ex 0.25
   public float healthgate = 3; //temp to not let Gatherer Die from using this ability
   public float cooldownTime = 5f; //creates a five second cooldown (for testing)
   private float lastUsedTime;

    public override string abilityName => "HealthTransfer";
    public override float duration => 0;

    public static AbilityHealthTransfer Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    void Awake()
    {
        InitSingleton();
    }
    public override void useAbility()
   {
      if (Time.time > lastUsedTime + cooldownTime)
      { //varifies that someone isn't spamming the q button and there is a gap between presses (Abiltiy Cooldown)
         if (StatsManager.Instance.GathererCurrentHealth > healthgate)
         { //Ability confirmed to be Q && Timer
           // transfer health from Gatherer to Wanderer
            lastUsedTime = Time.time;

            temp = StatsManager.Instance.GathererCurrentHealth * StatsManager.Instance.healthTransferAmount; // temp holds %25 percent of Gatherer's current health

            StatsManager.Instance.GathererCurrentHealth -= math.round(temp); // Subtract from current gatherer health



            StatsManager.Instance.WardenCurrentHealth += math.round(temp); //add to Wanderer Current Health

            if (StatsManager.Instance.WardenCurrentHealth > StatsManager.Instance.WardenMaxHealth)
            { 
               StatsManager.Instance.WardenCurrentHealth = StatsManager.Instance.WardenMaxHealth;
            }

            Debug.Log("Health of Gatherer:" + StatsManager.Instance.GathererCurrentHealth); //TESTING PURPOSES
            Debug.Log("Health of Warden:" + StatsManager.Instance.WardenCurrentHealth); //TESTING PURPOSES
            temp = 0f; //reset the health value stored (No longer needed health % can be different)
         }


      }
   }

}