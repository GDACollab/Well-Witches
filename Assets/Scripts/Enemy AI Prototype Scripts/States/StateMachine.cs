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
        enemy = GetComponent<BaseEnemyClass>();
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.wellArrive, this.transform.position);
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