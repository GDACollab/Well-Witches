using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class HealForcePassive : PassiveAbilities
{
    /*  Variables to change from statsManager
     *  public float GathererMaxHealth;
     *  public float GathererCurrentHealth;
     */

    public float regenTime;
    private float time;
    public override string abilityName => "HealForce";
    public static HealForcePassive Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake()
    {
        InitSingleton();
    }
    public override void passiveUpdate()
    {
        time = time + Time.deltaTime;
        if(time > regenTime)
        {
            EventManager.instance.playerEvents.PlayerDamage(-StatsManager.Instance.GathererHealthRegen, "Gatherer");
            if (StatsManager.Instance.GathererCurrentHealth > StatsManager.Instance.GathererMaxHealth)
            {
                StatsManager.Instance.GathererCurrentHealth = StatsManager.Instance.GathererMaxHealth;
                EventManager.instance.playerEvents.PlayerDamage(0, "Gatherer");
            }
            time = 0;
        }
    }
}
