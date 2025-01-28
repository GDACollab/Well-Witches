using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss : MonoBehaviour
{

    //Placeholder values
    [Header("Boss Stats")]
    [Range(1, 500)]
    [Tooltip("The max health of the boss enemy. [1, 500]")]
    public int health;
    [Range(0, 100)]
    [Tooltip("The damage the boss enemy deals. [0, 100]")]
    public int damage;
    [Range(0, 25)]
    [Tooltip("The base speed of the boss enemy. [0, 25]")]
    public int baseSpeed;

    //Movement (clicking for testing)
    Rigidbody2D rb;
    Vector3 click;
    Vector2 target;
    Vector2 pos;

    //BossActions
    Dictionary<int,String> bossAttacks = new Dictionary<int,String>();
    bossAttacks.Add(0,"Lunge");
    bossAttacks.Add(1,"ShieldBash");
    bossAttacks.Add(2,"Swing");
    bossAttacks.Add(3,"SpawnEnemies");

    //Coroutine stuff for paralell movement + actions
    private IEnumerator actRoutine;
    private IEnumerator moveRoutine;

    //Called upon the spawning of the boss
    void Start() 
    {
        rb.GetComponent<Rigidbody2D>();
        Act();
    }

    //Starts Boss Action Coroutines
    void Act() 
    {
        moveRoutine = Movement();
        actRoutine = Actions();
        StartCoroutine(moveRoutine)
        StartCoroutine(actRoutine)
    }

    //Continously Moves Boss Around Player
    IEnumerator Movement() 
    {
        while(true)
            yield return new WaitForSeconds(0.02f)
            {
                pos = new Vector2(transform.position.x, transform.position.y);
            // if left mouse button is clicked
            if (Input.GetMouseButtonDown(0)) {
                // get mouse click position
                click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target = new Vector2(click.x, click.y);
                // move towards target
                rb.velocity = (target - pos).normalized * moveSpeed;
            }
            // if enemy is close to target, stop moving
            if (Vector2.Distance(pos, target) < 0.5f) {
                rb.velocity = Vector2.zero;
            }

            // For the code below, assume the enemy sprite is facing left, the code is dependent on the direction of the enemy its facing
            // If direction.x (as calculated above) is positive, that means the enemy is on the left side of the player
            if (rb.velocity.x > 0)
            {
                // We dont make changes to the sprite since its already facing the player
                transform.localScale = new Vector3(1 ,1, 1);
            // If direction.x is negative (on the left of the player)
            } else if (rb.velocity.x < 0)
            {
                // We flip the enemy sprite so it faces the player
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        
    }

    //Continously assembles a list of boss actions in a randomized order which the boss follows one after another after a random time in a range 
    IEnumerator Actions() 
    {

    }
}