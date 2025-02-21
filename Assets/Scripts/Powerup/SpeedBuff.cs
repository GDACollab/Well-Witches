using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedBuff : PowerupEffect
{
   public int amount;
   public override void Apply(GameObject target)
   {

      StatsManager.Instance.speed += amount;
      Debug.Log("Player zooming at: " + StatsManager.Instance.speed.ToString());

   }
}
