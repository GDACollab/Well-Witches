using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
   public static StatsManager Instance; //singleton for both character stats

    /*
    With the StatsManager gameObject 
    it contains the stats in a nice and neat way (hopefully)
     */

    //buffs and buff timers
    public List<int> myBuffs = new List<int>();
    public List<float> buffTimers = new List<float>();


    [Header("---------------Gatherer Combat stats---------------")]
   public float healthTransferAmount; //current health transfer amount put in stats for now
   public float GathererResistance; 

   [Header("---------------Gatherer Movement stats---------------")]
   public float MaxSpeed; //connected to Movement script for testing
   public float acceleration; //based on movement scripts
   public float deacceleration; //based on movement scripts
   public float CurrentSpeed; //unused just using MaxSpeed for now


   [Header("---------------Gatherer Defense Stats---------------")]
   public float Defense; //current defense put in stats for now

   [Header ("---------------Gatherer Passive Stats---------------")]
   public float GathererHaste; //luck stat dunno just sounds cool
   public float GathererLuck;

   [Header("---------------Gatherer Health Stats---------------")]

   public float GathererMaxHealth;
   public float GathererCurrentHealth;

   public float GathererHealthRegen;



   [Header("---------------Warden Combat Stats---------------")]
   
   public float AbilityPower;

   public float AttackPower;

   public float Mana;

   public float CriticalChance;

   public float CriticalDamage;

   public float AttackSpeed;

   public float LifeSteal;

   [Header("---------------Warden Passive Stats---------------")]
   public float WardenHaste;
   public float WardenLuck;

   [Header("---------------Warden Defense Stats---------------")]

   public float WardenResistance; //might add a max resistance?

   [Header("---------------Warden Health Stats---------------")]

   public float WardenMaxHealth;
   public float WardenCurrentHealth;
   
   public float WardenHealthRegen;


   private void Awake()
   {
        if (Instance != null)
        {
            Debug.LogError("Found more than one GameManager in the scene. Please make sure there is only one");
        }
        Instance = this;
        
        //For Testing Purposes
        addStatus(5318008, 5);
    }

    private void Update()
    {
        for(int i = 0; i < buffTimers.Count; i++)
        {
            buffTimers[i] -= Time.deltaTime;
            Debug.Log("status " + myBuffs[i] + ": " + buffTimers[i]);
            if (buffTimers[i] <= 0)
            {
                Debug.Log("status " + myBuffs[i] + " is over!");
                myBuffs.RemoveAt(i);
                buffTimers.RemoveAt(i);
                i--;
            }
        }
    }

    public void addStatus(int buff, float time)
    {
        myBuffs.Add(buff);
        buffTimers.Add(time);
    }
}