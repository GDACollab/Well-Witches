using UnityEngine;
using System.Collections.Generic;

public class AIController : MonoBehaviour
{
    private StateMachine stateMachine;

    void Start()
    {
        // Initialize the state machine
        stateMachine = gameObject.GetComponent<StateMachine>();
        // Check if the state has been set before initializing the patrol state

        // Set the initial state (PatrolState), passing the player
        PatrolState patrolState = gameObject.GetComponent<PatrolState>();
        patrolState.Initialize(stateMachine, gameObject);
        stateMachine.SetState(patrolState);

    }

    void Update()
    {
        stateMachine.Update();  // Update the state machine each frame
    }
}