using UnityEngine;
using System.Collections.Generic;

public class AIController : MonoBehaviour
{
    private StateMachine stateMachine;
    private BaseEnemyClass enemy;
    void Start()
    {
        // Initialize the state machine
        stateMachine = gameObject.GetComponent<StateMachine>();

        BaseEnemyClass meleeEnemy = GetComponentInParent<MeleeEnemy>();
        BaseEnemyClass rangedEnemy = GetComponentInParent<RangedEnemy>();
        BaseEnemyClass tankEnemy = GetComponentInParent<TankEnemy>();
        enemy = null;
        if (meleeEnemy != null) { enemy = meleeEnemy; }
        else if (rangedEnemy != null) { enemy = rangedEnemy; }
        else if (tankEnemy != null) { enemy = tankEnemy; }

        // Set the initial state (AggroState), passing the player
        if (enemy != null && !enemy.isStunned)
        {
            AggroState AggroState = gameObject.GetComponent<AggroState>();
            AggroState.Initialize(stateMachine, gameObject);
            stateMachine.SetState(AggroState);
        }
    }

    void Update()
    {
        if (enemy != null && !enemy.isStunned)
        {
            stateMachine.Update();  // Update the state machine each frame
        }
    }
}