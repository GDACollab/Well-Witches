using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance; //singleton for both character stats

/*
With the StatsManager gameObject 
it contains the stats in a nice and neat way
 */

    [Header("Gatherer Combat stats")] 
    public float healthTransferAmount; //current health transfer amount put in stats for now

   [Header("Gatherer Movement stats")]
    public int speed;



    [Header("Gatherer Health stats")]

    public float GathererMaxHealth;
    public float GathererCurrentHealth;

    

    [Header("Wanderer Combat stats")]
    int attack;

    [Header("Wanderer Health stats")]

    public float wandererHealth;
    public float wandererMaxHealth;


private void Awake() {
        if (Instance == null) {
            Instance = this;
            
        } else {
            Destroy(this);
        }
    }
}
