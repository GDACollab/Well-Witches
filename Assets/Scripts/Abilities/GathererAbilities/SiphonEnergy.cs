using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphonEnergy : PassiveAbilities
{
    // Start is called before the first frame update
    public override string abilityName => "Siphon Energy";
    public static SiphonEnergy Instance { get; private set; }

    public bool moreEnergy = true;
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake()
    {
        InitSingleton();
    }
    public override void passiveUpdate()
    {
        //All this does is turn this ability on or off
        //Everytime an enemy dies, calls a method in the
        if (moreEnergy) { moreEnergy = false; }
        else { moreEnergy = true; }
    }
}
