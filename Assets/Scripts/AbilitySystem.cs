using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilitySystem : MonoBehaviour
{
    // Start is called before the first frame update

    public abstract void useAbility();
}

public class PowerOne : AbilitySystem
{


    public override void useAbility()
    {
        throw new System.NotImplementedException();
    
    }
}
