using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
   public static StatsManager Instance; //singleton for both character stats

   /*
   With the StatsManager gameObject 
   it contains the stats in a nice and neat way
    */

   [Header("Gatherer Combat stats")]
   public float healthTransferAmount; //current health transfer amount put in stats for now
   public float Resistance;

   [Header("Gatherer Movement stats")]
   public float speed;

   [Header("Gatherer Defense stats")]
   public float Defense; //current defense put in stats for now


   [Header("Gatherer Health stats")]

   public float Gatherer_MaxHealth;
   public float Gatherer_CurrentHealth;



   [Header("Wanderer Combat stats")]
   public float Ability_Power;

   public float Critical_Chance;

   public float Critical_Damage;

   public float Attack_Speed;

   public float Haste;

   [Header("Wanderer Defense stats")]

   public float Wanderer_Resistance; //might add a max resistance?

   [Header("Wanderer Health stats")]

   public float wanderer_MaxHealth;
   public float wanderer_Health;


   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;

      }
      else
      {
         Destroy(this);
      }
   }
}