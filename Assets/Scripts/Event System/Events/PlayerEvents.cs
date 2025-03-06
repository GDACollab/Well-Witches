using System;

public class PlayerEvents
{
    public event Action<float,string> onPlayerDamaged;
	public event Action onPlayerDied;

	public void PlayerDamaged(float damage, string player)
    {
        if (onPlayerDamaged != null)  onPlayerDamaged?.Invoke(damage, player);
    }

    public void PlayerDied()
    {
        if (onPlayerDamaged != null) onPlayerDied?.Invoke();
    }
}
