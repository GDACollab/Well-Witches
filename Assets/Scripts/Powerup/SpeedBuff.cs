using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedBuff : PowerupEffect
{
   public float amount;
   public override void Apply(GameObject target)
   {
      target.GetComponent<PlayerController>().maxSpeed_Adjusted += amount;
        Debug.Log("Player zooming at: " + target.GetComponent<PlayerController>().maxSpeed_Adjusted.ToString());
   }
}
