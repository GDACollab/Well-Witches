using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbilities : MonoBehaviour
{
    public abstract string abilityName { get; }

    public abstract void passiveUpdate();

}
