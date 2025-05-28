using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphonEnergy : PassiveAbilities
{
    // Start is called before the first frame update
    public override string abilityName => "SiphonEnergy";
    public static SiphonEnergy Instance { get; private set; }

    public ParticleSystem VFXPrefab;
    private ParticleSystem effect;
    //change much % of the total you gain
    public float percent;
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake()
    {
        InitSingleton();
        effect = Instantiate(VFXPrefab, transform);
    }
    public override void passiveUpdate()
    {
        // nothing
    }

    //add energy method
    public void AddEnergy() {
        WardenAbilityManager.Instance.equipedAbility.Charge = WardenAbilityManager.Instance.equipedAbility.Charge + ((percent/100) * WardenAbilityManager.Instance.equipedAbility.numHitsRequired);
        if (WardenAbilityManager.Instance.equipedAbility.Charge > WardenAbilityManager.Instance.equipedAbility.numHitsRequired) {
            WardenAbilityManager.Instance.equipedAbility.Charge = WardenAbilityManager.Instance.equipedAbility.numHitsRequired;
        }
        effect.Play();
    }

}
