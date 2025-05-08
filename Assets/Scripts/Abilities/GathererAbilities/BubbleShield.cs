using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    public void Activate(float duration)
    {
        Destroy(gameObject, duration);
    }
}
