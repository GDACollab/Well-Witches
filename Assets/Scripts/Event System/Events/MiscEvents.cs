using System;
public class MiscEvent
{
    public event Action onGarlicCollected;
    public event Action onPumpkinCollected;

    public void GarlicCollected()
    {
        if (onGarlicCollected != null)
        {
            onGarlicCollected();
        }
    }
    public void PumpkinCollected(){
        if (onPumpkinCollected != null){
            onPumpkinCollected();
        }
    }
}
