using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public abstract void InitState();
    public abstract void UpdateState();
}
