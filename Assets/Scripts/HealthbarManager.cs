using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarManager : MonoBehaviour
{
    [SerializeField]
    private Slider HealthSlider;
    private int maxHealth = 100;
    private int currentHealth = 100;

    // FOR TESTING PURPOSES ONLY
    /*
    void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            DecreaseHealth(1);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            IncreaseHealth(1);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            SetHealth(50);
        }
    }
    */

    public int GetCurrentHealth(){
        return currentHealth;
    }

    public void IncreaseHealth(int amount){
        currentHealth = Math.Min(currentHealth+amount, maxHealth);
        UpdateHealthbar();
    }

    public void DecreaseHealth(int amount){
        currentHealth = Math.Max(currentHealth-amount, 0);
        UpdateHealthbar();
    }

    public void SetHealth(int newHealth){
        newHealth = Mathf.Clamp(newHealth, 0, maxHealth);
        currentHealth = newHealth;
        UpdateHealthbar();
    }

    private void UpdateHealthbar(){
        HealthSlider.value = currentHealth;
    }
}
