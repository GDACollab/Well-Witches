using System;
public class MiscEvent
{
    public event Action onGarlicCollected;
    public event Action onPumpkinCollected;
    public event Action onShowAbilityUI;

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
    
    public void ShowAbilityUI()
    {
        if (onShowAbilityUI != null){
            onShowAbilityUI();
        }
    }
}
