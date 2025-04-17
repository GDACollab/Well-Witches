using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SolesOfTheDamned : PassiveAbilities
{
    // Start is called before the first frame update
    public override string abilityName => "SolesOfTheDamned";
    [SerializeField]
    public float duration;
    [SerializeField]
    public float damage;
    [SerializeField]
    public float flamesPerSecond;
    public bool startAbility = false;
    void Start()
    {
        
    }
    public override void passiveUpdate() { 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
