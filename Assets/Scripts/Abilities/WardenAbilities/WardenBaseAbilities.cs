using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WardenBaseAbilities : MonoBehaviour
{
    // An abstract class in C# acts like a skeleton that derived classes must fill out (derived class: a class that inherits properties and methods from another class)

    // This means that any ability you make that inherits from this class MUST implement the below function
    // Classes that inherit from this must also set the abilityName, numHitsRequired, and duration

    // An ability that uses this:
    /*
     * public class WardenNuke : WardenBaseAbilities
     * {
     *      public override string abilityName => "Nuke";
     *      public override int numHitsRequired => 35;
     *      public override float duration => 2.0f;
     *      
     *      public static WardenNuke Instance { get; private set; }
     *      
     *      void Awake()
     *      {
     *          if (Instance && Instance != this) Destroy(gameObject); 
     *          else Instance = this;
     *      }
     *      
     *      public override void useAbility()
     *      {
     *          *Code here that starts the ability*
     *      }
     *      
     *      you can write other functions within your class that useAbility() can rely on, you just have to make sure
     *      useAbility() is the one that actually starts the ability.
     * }
    */
    public abstract void useAbility();

    public abstract string abilityName { get; }
    public abstract int numHitsRequired { get; }
    public abstract float duration { get; }
    public abstract float Charge { get; set; }

    protected IEnumerator CastSpell()
    {
        Animator animator = GetComponentInChildren<Animator>();
        animator.SetBool("isCasting", true);
        yield return new WaitForSeconds(duration);
        animator.SetBool("isCasting", false);
    }
}
