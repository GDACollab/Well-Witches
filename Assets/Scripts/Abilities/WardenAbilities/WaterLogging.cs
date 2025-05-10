using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLogging : PassiveAbilities
{
    public override string abilityName => "WaterLogging";
    public static WaterLogging Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    public float duration;
    //how much % slower so if waterspeed is 0.8 the enemy will be 20% slower
    public float speed;

    private void Awake()
    {
        InitSingleton();
    }

    void Start()
    {
        WardenAbilityManager.Instance.waterDuration = duration;
        WardenAbilityManager.Instance.waterSpeed = speed;
    }

    public override void passiveUpdate()
    {
        
    }

}
