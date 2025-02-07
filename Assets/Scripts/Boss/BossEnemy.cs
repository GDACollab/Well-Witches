using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Phases in battle
enum BossPhase {
	Phase1,
	Phase2
}

/*
Boss fight:
3 actions:
	- Lunge
		Set target past player, up move speed a ton
	- Attack ("sheild bash"/"swing")
		Hit box in front of boss
	- Spawn Enemy
		Reference spawner script in scene

Health bar: Link to UI element
*/

public class BossEnemy : MonoBehaviour
{
	private StateMachine stateMachine;

    // --- Boss States ---
    [Header("Boss Stats")]

	// Health
    [Range(1, 500)]
    [Tooltip("The max health of the boss enemy. [1, 500]")]
    public int maxHealth;
	int health;
    [Range(0, 100)]

	// Base damage (moves will have multipliers from this)
    [Tooltip("The base damage the boss enemy deals. [0, 100]")]
    public int damage;
    [Range(0, 25)]

	// Base speed
    [Tooltip("The base speed of the boss enemy. [0, 25]")]
    public int baseSpeed;

	// --- Actions ---
	BossPhase currPhase;

	/* Boss Attacks:
	* Lunge: 30
	* Shield Bash: 25
	* Swing: 30
	* Spawn Enemies: 40
	*/
	int[] attacksWeight = {30, 25, 30, 40};

    int attackChosen;
    int totalWeight;

    //Coroutine stuff for paralell movement + actions
    private IEnumerator actRoutine;

    //Called upon the spawning of the boss
    void Start() 
    {
		health = maxHealth;
        currPhase = BossPhase.Phase1;

		// --- Initialize AI ---
		// Initialize the state machine
        stateMachine = gameObject.GetComponent<StateMachine>();
        // Check if the state has been set before initializing the patrol state

        // Set the initial state (PatrolState), passing the player
        IdleState idleState = gameObject.GetComponent<IdleState>();
        idleState.Initialize(stateMachine, gameObject);
        stateMachine.SetState(idleState);

		// TODO: Convert placeholder PickAction script to a transistion table for special attack states
        PickAction();
    }

	public void takingDamage(int amount)
	{   //Reduces health by the amount entered in Unity
		health -= amount;
		if (health <= 50){
			currPhase = BossPhase.Phase2;
		}
		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}

    //Continously assembles a list of boss actions in a randomized order which the boss follows one after another after a random time in a range 
    int PickAction() 
    {
		//attacksWeight[0] = 30 * (1 + Vector2.Distance(pos, target)/100);
		//attacksWeight[1] = 25 * (1 + 1/sqrt(Vector2.Distance(pos, target) * 1.05));
		//attacksWeight[2] = 30 * (1 + 1/Vector2.Distance(pos, target) * 1.2);
		//attacksWeight[3] = 40 * currPhase;

		totalWeight = attacksWeight[0] + attacksWeight[1] + attacksWeight[2] + attacksWeight[3];
		int rand = Random.Range(1, totalWeight);

		// Select action to do
		if (rand < attacksWeight[0])
		{
			return 0;
		}
		else if (rand < attacksWeight[2])
		{
			return 1;
		}
		else if (rand < attacksWeight[3])
		{
			return 2;
		}
		return 0;
	}
}