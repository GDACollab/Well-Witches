using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class CoolDown

{
   [SerializeField] private float cooldownTime;

   private float _nextUsableTime;


   public bool IsCoolingDown => Time.time < _nextUsableTime;
   public void StartCooldown() => _nextUsableTime = Time.time + cooldownTime;
}
