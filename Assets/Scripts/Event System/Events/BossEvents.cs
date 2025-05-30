using System;


public class BossEvents
{
    public event Action onBushCollected;

    public void BushCollected()
    {
        if (onBushCollected != null)
        {
            onBushCollected();
        }
    }

    public event Action onBushReset;

    public void BushReset()
    {
        if (onBushReset != null)
        {
            onBushReset();
        }
    }
}
