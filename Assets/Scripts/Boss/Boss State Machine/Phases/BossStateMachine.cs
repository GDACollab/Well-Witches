using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public State currentState;

    public void SetState(State newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = newState;

        Debug.Log("Entering New State: " + currentState.ToString());

        if (currentState != null)
        {
            currentState.OnEnter();

        }
    }

    public void Update()
    {
        if (currentState != null)
        {
            // Check transitions before updating the current state
            foreach (Transition transition in currentState.GetTransitions())
            {
                if (transition.ShouldTransition())
                {
                    SetState(transition.GetNextState());
                    return;
                }
            }

            // Update the current state
            currentState.OnUpdate();
        }
    }
}