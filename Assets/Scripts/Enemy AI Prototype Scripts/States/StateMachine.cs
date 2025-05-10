using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;
    private BaseEnemyClass enemy;

    void Start()
    {
        BaseEnemyClass meleeEnemy = GetComponentInParent<MeleeEnemy>();
        BaseEnemyClass rangedEnemy = GetComponentInParent<RangedEnemy>();
        BaseEnemyClass tankEnemy = GetComponentInParent<TankEnemy>();
        enemy = null;
        if (meleeEnemy != null) { enemy = meleeEnemy; }
        else if (rangedEnemy != null) { enemy = rangedEnemy; }
        else if (tankEnemy != null) { enemy = tankEnemy; }
    }

    public void SetState(State newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = newState;


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

            // Update the current state`
            currentState.OnUpdate();
        }
    }
}