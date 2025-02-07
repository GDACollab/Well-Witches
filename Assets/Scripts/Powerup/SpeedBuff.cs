using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedBuff : PowerupEffect
{
   public float amount;
   public override void Apply(GameObject target)
   {

      //target.GetComponent<PlayerController_Justin>().moveSpeed += amount;
      //Debug.Log("Player zooming at: " + target.GetComponent<PlayerController_Justin>().moveSpeed.ToString());

   }
}
