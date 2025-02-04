using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossEnemy : MonoBehaviour, BaseEnemyClass
{

    // //Placeholder values
    // [Header("Boss Stats")]
    // [Range(1, 500)]
    // [Tooltip("The max health of the boss enemy. [1, 500]")]
    // public int health;
    // [Range(0, 100)]
    // [Tooltip("The damage the boss enemy deals. [0, 100]")]
    // public int damage;
    // [Range(0, 25)]
    // [Tooltip("The base speed of the boss enemy. [0, 25]")]
    // public int baseSpeed;

    // Phases in battle
    private enum Phase {
        Phase1,
        Phase2
    }

    Phase currPhase;
    
    [SerializeField] private EnemySpawner enemySpawner;

    //Movement (clicking for testing)
    //Rigidbody2D rb;
    //Vector3 click;
    //Vector2 target;
    //Vector2 pos;

    //BossActions
    Dictionary bossAttacks = new Dictionary<int,String>();
    bossAttacks.Add(0,"Lunge");
    bossAttacks.Add(1,"ShieldBash");
    bossAttacks.Add(2,"Swing");
    bossAttacks.Add(3,"SpawnEnemies");

    Dictionary attacksWeight = new Dictionary<int, int>();
    attacksWeight.Add(0, 30);
    attacksWeight.Add(1, 25);
    attacksWeight.Add(2, 30);
    attacksWeight.Add(3, 40);
    int attackChosen;
    int totalWeight;

    //Coroutine stuff for paralell movement + actions
    private IEnumerator actRoutine;
    private IEnumerator moveRoutine;

    //Called upon the spawning of the boss
    void Start() 
    {
        currPhase = Phase1;
        rb.GetComponent<Rigidbody2D>();
        Act();
    }

    void FixedUpdate(){
        if (health <= 50){
            currPhase = Phase2;
        }
    }

    //Starts Boss Action Coroutines
    void Act() 
    {
        moveRoutine = Movement();
        actRoutine = Actions();
        StartCoroutine(moveRoutine);
        StartCoroutine(actRoutine);
    }

    //Continously Moves Boss Around Player
    // IEnumerator Movement() 
    // {
    //     while(true)
    //         yield return new WaitForSeconds(0.02f)
    //         {
    //             pos = new Vector2(transform.position.x, transform.position.y);
    //         // if left mouse button is clicked
    //         if (Input.GetMouseButtonDown(0)) {
    //             // get mouse click position
    //             click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //             target = new Vector2(click.x, click.y);
    //             // move towards target
    //             rb.velocity = (target - pos).normalized * moveSpeed;
    //         }
    //         // if enemy is close to target, stop moving
    //         if (Vector2.Distance(pos, target) < 0.5f) {
    //             rb.velocity = Vector2.zero;
    //         }

    //         // For the code below, assume the enemy sprite is facing left, the code is dependent on the direction of the enemy its facing
    //         // If direction.x (as calculated above) is positive, that means the enemy is on the left side of the player
    //         if (rb.velocity.x > 0)
    //         {
    //             // We dont make changes to the sprite since its already facing the player
    //             transform.localScale = new Vector3(1 ,1, 1);
    //         // If direction.x is negative (on the left of the player)
    //         } else if (rb.velocity.x < 0)
    //         {
    //             // We flip the enemy sprite so it faces the player
    //             transform.localScale = new Vector3(-1, 1, 1);
    //         }
    //     }
        
    // }

    //Continously assembles a list of boss actions in a randomized order which the boss follows one after another after a random time in a range 
    IEnumerator Actions() 
    {
        attacksWeight[0] = 30 * (1 + Vector2.Distance(pos, target)/100);
        attacksWeight[1] = 25 * (1 + 1/sqrt(Vector2.Distance(pos, target) * 1.05));
        attacksWeight[2] = 30 * (1 + 1/Vector2.Distance(pos, target) * 1.2);
        attacksWeight[3] = 40 * currPhase;

        totalWeight = attacksWeight[0] + attacksWeight[1] + attacksWeight[2] + attacksWeight[3];
        int rand = Random.Range(1, totalWeight);

        foreach(KeyValuePair<int, int> attackWeight in attacksWeight) 
        {

        }

    }
    
    void SpawnMobs()
    {
        // Spawn an enemy from the scene near the player
        enemySpawner.SpawnSingleFormation(UnityEngine.Random.Range(3f, 8f));
    }
}