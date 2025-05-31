using System;
using UnityEngine;

public class PlayerEvents
{
    private bool disableDamage = false;
    public event Action<float,string> onPlayerDamage;
    
    public void PlayerDamage(float damage, string player)
    {
        if (disableDamage)
        {
            if (player == "Gatherer" && onPlayerDamage != null)
            {
                onPlayerDamage(damage, player);
            }
            return;
        }

        if (onPlayerDamage != null)
        {
            onPlayerDamage(damage, player);
        }
    }

    public event Action onPlayerDeath;

    public void PlayerDeath()
    {
        if (onPlayerDeath != null)
        {
            onPlayerDeath();
        }
    }

    public void DisableDamage(bool on)
    {
        if (on)
        {
            disableDamage = true;
        }else
        {
            disableDamage = false;
        }
    }
}
