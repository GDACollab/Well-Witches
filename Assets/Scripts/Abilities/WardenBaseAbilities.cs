using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WardenBaseAbilities : MonoBehaviour
{
    public abstract void useAbility();
    public string abilityName;
    protected int numHitsRequired;
    protected float duration;

}
