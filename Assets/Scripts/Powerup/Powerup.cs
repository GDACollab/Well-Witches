using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Powerup : MonoBehaviour //monobehavior attact to game object
{
   public PowerupEffect powerupEffect;
   private void OnTriggerEnter2D(Collider2D collision)
   {
      //add check for player only later
      Destroy(gameObject);
      powerupEffect.Apply(collision.gameObject);
      
   }
}

