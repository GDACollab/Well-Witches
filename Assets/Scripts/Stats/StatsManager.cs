using System;
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

    //Gets references to each individual health bars
    [SerializeField] HealthBar Warden;
    [SerializeField] HealthBar Gatherer;
    //buffs and buff timers



    [Header("---------------Buff / Curse Tracking---------------")]
    public List<string> myBuffs = new List<string>();
    public List<float> buffTimers = new List<float>();

    public Dictionary<string, float> questItems = new Dictionary<string, float>
    {
        {"Dullahan’s Head", 0.15f},
        {"Vampire Garlics", 0.10f}
    };

    public float keyItemChance = 0.05f;


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

    public void addStatus(string buff, float time)
    {
        myBuffs.Add(buff);
        buffTimers.Add(time);
    }

    public Dictionary<string, float> getQuestItems() 
    {
        return new Dictionary<string, float> (questItems);
    }

    public List<string> getMyBuffs() 
    {
        return new List<string> (myBuffs);
    }

    public float getKeyItemChance()
    {
        return keyItemChance;
    }

    //Public func to calculate taking damage and updating healthbars.
    //Sends a signal to ondie to the individual warden/gatherer movement scripts to stop
    //This may be temp until event bus/script is figured out (sorry - ben)
    public void tookDamage(string player, float damage)
    {
        if (player == "Gatherer")
        {
            GathererCurrentHealth = GathererCurrentHealth - damage;
            Gatherer.UpdateHealthBar(GathererCurrentHealth, GathererMaxHealth);
            Debug.Log(GathererCurrentHealth);
        }
        else if (player == "Warden")
        {
            WardenCurrentHealth = WardenCurrentHealth - damage;
            Warden.UpdateHealthBar(WardenCurrentHealth, WardenMaxHealth);

        }
        else
        {
            Debug.Log("Somethign has gone very god dam wrong");
        }
    }
}