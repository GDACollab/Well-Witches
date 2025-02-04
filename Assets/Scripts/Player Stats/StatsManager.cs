using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    //gatherer first?

    [Header("Gatherer Combat stats")]
    public int healthTransferAmount;

    [Header("Gatherer Movement stats")]
    public int speed;



    [Header("Gatherer Health stats")]

    public int GathererMaxHealth;
    public int GathererCurrentHealth;

    

    [Header("Wanderer Combat stats")]
    int attack;

    [Header("Wanderer Health stats")]

    public int wandererHealth;
    public int wandererMaxHealth;


private void Awake() {
        if (Instance == null) {
            Instance = this;
            
        } else {
            Destroy(this);
        }
    }
}
