using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    public enum StateName
    {
        State0,
        State1,
        State2
    }

    // Modify this variable using the Unity dropdown or from another class when changing the state
    public StateName requestedState;

    // This variable keeps track of the name of the state the player had been in until now
    private StateName currentState;

    // Stores all possible states of the character
    public PlayerState[] states;


    // Start is called before the first frame update
    void Start()
    {
        currentState = requestedState;
    }


    // Update is called once per frame
    void Update()
    {
        if (requestedState != currentState) {
            currentState = requestedState;
            states[(int)currentState].InitState();
        }
        states[(int)currentState].UpdateState();
    }
}
