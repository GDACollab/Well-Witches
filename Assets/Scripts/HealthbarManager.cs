using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
            DecreaseCurrentHealth(1);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            IncreaseCurrentHealth(1);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            SetCurrentHealth(50);
        }
        if(Input.GetKeyDown(KeyCode.F)){
            SetMaxHealth(300);
        }
    }
    */

    public int GetCurrentHealth(){
        return currentHealth;
    }

    public void IncreaseCurrentHealth(int amount){
        currentHealth = Math.Min(currentHealth+amount, maxHealth);
        UpdateHealthbar();
    }

    public void DecreaseCurrentHealth(int amount){
        currentHealth = Math.Max(currentHealth-amount, 0);
        UpdateHealthbar();
    }

    public void SetCurrentHealth(int newHealth){
        newHealth = Mathf.Clamp(newHealth, 0, maxHealth);
        currentHealth = newHealth;
        UpdateHealthbar();
    }

    public void SetMaxHealth(int newMaxHealth){
        if(newMaxHealth < 1){
            Debug.Log("Attempted to set max health to 0 or a negative number");
            return;
        }
        maxHealth = newMaxHealth;
        HealthSlider.maxValue = newMaxHealth;
        UpdateHealthbar();
    }

    private void UpdateHealthbar(){
        HealthSlider.value = currentHealth;
    }
}
