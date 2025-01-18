using UnityEngine;
using System.Collections.Generic;

public class AIController : MonoBehaviour
{
    private StateMachine stateMachine;
    public GameObject player;   // Assign this in the Inspector or dynamically
    public GameObject enemy;    // Reference to the enemy

    void Start()
    {
        // Initialize the state machine
        stateMachine = new StateMachine();
    }

    void Update()
    {
        // Check if the state has been set before initializing the patrol state
        if (stateMachine.currentState == null)
        {
            // Set the initial state (PatrolState), passing the player
            PatrolState patrolState = new PatrolState(stateMachine, enemy, player);
            stateMachine.SetState(patrolState);
        }

        stateMachine.Update();  // Make sure to update the state machine each frame
    }
}