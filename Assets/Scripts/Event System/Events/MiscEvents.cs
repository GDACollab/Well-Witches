using System;
public class MiscEvent
{
    public event Action onGarlicCollected;

    public void GarlicCollected()
    {
        if (onGarlicCollected != null)
        {
            onGarlicCollected();
        }
    }
}
