using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : State
{
    public bool isStunned;
    private StateMachine stateMachine;
    private MeleeEnemy meleeEnemy;
    private RangedEnemy rangedEnemy;
    private TankEnemy tankEnemy;
    private NavMeshAgent navAgent;

    [SerializeField] private float stunTime;
    private float stunStartTime;

    public StunnedState(GameObject owner) : base(owner) { }

    public void Initialize(StateMachine stateMachine, GameObject owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
        navAgent = owner.GetComponent<NavMeshAgent>();
    }
    
    public override void OnEnter()
    {
        Debug.Log("stunned!");
        isStunned = true;
        navAgent.enabled = false;   // freeze them by turning navAgent off
        stunTime = 5.0f;    // 5 second stun; change this value as necessary
        stunStartTime = Time.time;  // deltaTime doesn't work in OnUpdate apparently :/
    }

    public override void OnUpdate()
    {
        // play a stun animation if we have that?
        if (isStunned && (Time.time - stunStartTime) >= stunTime)
        {
            Debug.Log("exiting stun from StunState script");
            OnExit();   // maybe the timer should be kept track of somewhere else?
        }
    }

    public override void OnExit()
    {
        Debug.Log("unstunned");
        isStunned = false;
        navAgent.enabled = true;
    }

    public override List<Transition> GetTransitions()
    {
        return new List<Transition> 
        { 
            new OutotRangeTransition(stateMachine, owner)
        };
    }
}
