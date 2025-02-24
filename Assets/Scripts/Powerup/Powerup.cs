using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Powerup : MonoBehaviour //monobehavior attact to game object
{
   public PowerupEffect powerupEffect;

   public Collider2D PlayerColider;
   
   private void OnTriggerEnter2D(Collider2D collision)
   {
      if(collision == PlayerColider) { //added a check so only the player can pickup what this script can attach too
      Destroy(gameObject);
      powerupEffect.Apply(collision.gameObject);
      }
   }
}

