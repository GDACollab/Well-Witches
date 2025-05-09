using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class SiphonEnergy : PassiveAbilities
{
    // Start is called before the first frame update
    public override string abilityName => "SiphonEnergy";
    public static SiphonEnergy Instance { get; private set; }

    //helps keep code clean
    private WardenAbilityManager abilityManager = WardenAbilityManager.Instance;
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake()
    {
        InitSingleton();
    }
    public override void passiveUpdate()
    {
        //if siphonTimes is greater than 0 then add 3% of total energy
        if (abilityManager.siphonTimes > 0) {
            addEnergy();
            abilityManager.siphonTimes--;
        }
    }

    //add energy method
    public void addEnergy() {
        abilityManager.equipedAbility.Charge = abilityManager.equipedAbility.Charge + (0.03f * abilityManager.equipedAbility.numHitsRequired);
        if (abilityManager.equipedAbility.Charge > abilityManager.equipedAbility.numHitsRequired) {
            abilityManager.equipedAbility.Charge = abilityManager.equipedAbility.numHitsRequired;
        }
    }

}
