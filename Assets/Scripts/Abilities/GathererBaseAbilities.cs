using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GathererBaseAbilities : MonoBehaviour
{
    public abstract void useAbility();
    public string abilityName;
    public int durationOfAbility; //Time of how long the ability lasts
    public float chargeTime; //Time required to charge active ability
}
