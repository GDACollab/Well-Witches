using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected GameObject owner;

    public State(GameObject owner)
    {
        this.owner = owner;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();

    // Define a method to get transitions for the state
    public abstract List<Transition> GetTransitions();
}