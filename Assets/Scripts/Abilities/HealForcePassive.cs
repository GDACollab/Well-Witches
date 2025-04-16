using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class HealForcePassive : MonoBehaviour
{
    /*  Variables to change from statsManager
     *  public float GathererMaxHealth;
     *  public float GathererCurrentHealth;
     */

    public float healthRegen;
    public float regenTime;
    private float time;

    private void Update()
    {
        time = time + Time.deltaTime;
        if(time > regenTime)
        {
            StatsManager.Instance.GathererCurrentHealth +=  healthRegen;
            if (StatsManager.Instance.GathererCurrentHealth > StatsManager.Instance.GathererMaxHealth)
            {
                StatsManager.Instance.GathererCurrentHealth = StatsManager.Instance.GathererMaxHealth;
            }
        }
    }
}
