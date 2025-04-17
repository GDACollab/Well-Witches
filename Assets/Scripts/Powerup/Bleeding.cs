using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Bleeding : MonoBehaviour
{
     
    private int playerHealth = 10;
    private float bleedingdamagepersecond = 0.5f; // 0.5%
    
    public void applyDamage(){
        //This will apply the damage to the player over time, like a damage over time effect
        playerHealth -= (int)(playerHealth * bleedingdamagepersecond);
    }
    public void Update()
    {
        Debug.Log("Bleeding");
        applyDamage();
    }
}
