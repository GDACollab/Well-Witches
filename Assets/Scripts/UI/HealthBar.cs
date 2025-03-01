using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Refrences")]
    private Slider healthbar;

    private void Start()
    {
        healthbar = this.GetComponentInChildren<Slider>();
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthbar.value = currentHealth / maxHealth;
    }
}
