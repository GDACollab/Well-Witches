using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SpeedBuff")]
public class SpeedBuff : PowerupEffect
{
   public float amount;
   public override void Apply(GameObject target)
   {
      StatsManager.Instance.MaxSpeed += amount;
        Debug.Log("Player zooming at: " + StatsManager.Instance.MaxSpeed.ToString());
   }
}
