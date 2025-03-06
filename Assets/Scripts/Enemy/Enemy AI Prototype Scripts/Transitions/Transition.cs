using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition
{
    protected GameObject owner;

    public Transition(GameObject owner)
    {
        this.owner = owner;
    }

    public abstract bool ShouldTransition();
    public abstract State GetNextState();
}