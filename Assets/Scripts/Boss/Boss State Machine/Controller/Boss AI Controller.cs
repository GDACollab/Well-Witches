using UnityEngine;
using System.Collections.Generic;

public class BossAIController : MonoBehaviour
{
    private StateMachine stateMachine;
    void Start()
    {
        // Initialize the state machine
        stateMachine = gameObject.GetComponent<StateMachine>();

        // Set the initial state (AggroState), passing the player
        PhaseOne PhaseOne = gameObject.GetComponent<PhaseOne>();
        PhaseOne.Initialize(stateMachine, gameObject);
        stateMachine.SetState(PhaseOne);

    }

    void Update()
    {
        stateMachine.Update();  // Update the state machine each frame
    }
}