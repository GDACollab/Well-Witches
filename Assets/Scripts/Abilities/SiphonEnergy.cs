using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphonEnergy : PassiveAbilities
{
    // Start is called before the first frame update
    public override string abilityName => "SiphonEnergy";
    public static SiphonEnergy Instance { get; private set; }

    private WardenBaseAbilities activeAbility;

    public bool moreEnergy = true;
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake()
    {
        InitSingleton();

        // Get active ability
        activeAbility = WardenAbilityManager.Instance.equipedAbility;
    }
    public override void passiveUpdate()
    {
        //All this does is turn this ability on or off
        //Everytime an enemy dies, method Siphon() is called
        if (moreEnergy) { moreEnergy = false; }
        else { moreEnergy = true; }
    }

    // Adds additional charge (3%) to the warden's active ability meter
    public void Siphon()
    {
        activeAbility.Charge += activeAbility.numHitsRequired / 33; // ~3% charge
    }
}
