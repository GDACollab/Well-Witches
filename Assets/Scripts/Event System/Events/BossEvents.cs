using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
