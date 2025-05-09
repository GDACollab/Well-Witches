using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLogging : PassiveAbilities
{
    public override string abilityName => "WaterLogging";
    public static WaterLogging Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    public float duration;

    private void Awake()
    {
        InitSingleton();
    }

    public override void passiveUpdate()
    {
        
    }

    public void wet(BaseEnemyClass enemy) { 
    }
}
