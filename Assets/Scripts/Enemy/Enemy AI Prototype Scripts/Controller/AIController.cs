using UnityEngine;

public class AIController : MonoBehaviour
{
    private StateMachine stateMachine;
    void Start()
    {
        // Initialize the state machine
        stateMachine = gameObject.GetComponent<StateMachine>();

        // Set the initial state (AggroState), passing the player
        AggroState AggroState = gameObject.GetComponent<AggroState>();
        AggroState.Initialize(stateMachine, gameObject);
        stateMachine.SetState(AggroState);

    }

    void Update()
    {
        stateMachine.Update();  // Update the state machine each frame
    }
}