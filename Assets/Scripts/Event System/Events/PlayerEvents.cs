using System;
using UnityEngine;

public class PlayerEvents
{
    public event Action<float,string> onPlayerDamage;
    
    public void PlayerDamage(float damage, string player)
    {
        if (onPlayerDamage != null)
        {
            onPlayerDamage(damage, player);
        }
    }

    public event Action onPlayerDeath;

    public void PlayerDeath()
    {
        if (onPlayerDamage != null)
        {
            onPlayerDeath();
        }
    }
}
