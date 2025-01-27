using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoodEvent : MonoBehaviour
{
   public enum Buffs {healthUp, attackUp};
   //public enum Debuffs {healthDown, attackDown};

   public float buffAmount;

   private WardenStats warden;

   public Buffs buff;
   //public Debuffs debuff;

   private void Awake(){
      warden = FindObjectOfType<WardenStats>();
   }

   private void OnTriggerEnter2D(Collider2D collision){
      if(collision.gameObject.tag == "Player"){
         switch (buff)
         {
            case Buffs.healthUp:
               warden.health += buffAmount;
               Debug.Log("Health Buffed by: " + warden.health);
               break;
            case Buffs.attackUp:
               warden.attackPower += buffAmount;
               Debug.Log("Attack Buffed by: " + warden.attackPower);
               break;
            default:
               break;
         }
      }

        Destroy(gameObject);
   }

}
