using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PowerupEffect : ScriptableObject //abstract we build
{                                        //powerups out of
   public abstract void Apply(GameObject target);
}
