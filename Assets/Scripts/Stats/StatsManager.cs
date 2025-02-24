using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Healthbars are displayed on canvas.
/// Health bars track a gameobjects transform
/// </summary>
public class StatsManager : MonoBehaviour
{

    public static StatsManager Instance; //singleton for both character stats


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    /*
    With the StatsManager gameObject 
    it contains the stats in a nice and neat way
     */

    [Header("Gatherer stats")] 
    public float healthTransferAmount; //current health transfer amount put in stats for now 
    public float gathererMaxHealth;
    [HideInInspector] public float gathererCurrentHealth;
    //public float gathererSpeed;

    [Header("Wanderer Combat stats")]
    public float wardenattack;
    public float wardenMaxHealth;
    [HideInInspector] public float wardenHealth;
    //public float wardenSpeed;

    [Header("Stat Modifiers")]
    [SerializeField] private float healthBuff = 0f;
    [SerializeField] private float staminaBuff = 0f;
    [SerializeField] private float speedBuff = 0f;
    [SerializeField] private float attackBuff = 0f;

    [Header("Stats for both")]
    public float speed; // Does nothing atm, could integrate with movement by having movement script read this?

    //Fetchers for private stats, stats are public for now may change
    //Gatherer fetchers
    public float GetGealthTransferAmount() { return healthTransferAmount; }
    public float GetGathererMaxHealth() { return gathererMaxHealth; }
    public float GetGathererCurrentHealth() { return gathererCurrentHealth; }

    //Warden fetchers
    public float GetWardenattack() { return wardenattack; }
    public float GetWardenMaxHealth() { return wardenMaxHealth; }
    public float GetwardenHealth() { return wardenHealth; }

    //Buff fetchers
    public float GetHealthBuff() {return healthBuff; }
    public float GetStaminaBuff() { return staminaBuff; }
    public float GetSpeedBuff() { return speedBuff; }
    public float GetAttackBuff() { return attackBuff; }

    private void Start()
    {
        gathererCurrentHealth = gathererMaxHealth;
        wardenHealth = wardenMaxHealth;
    }
}
