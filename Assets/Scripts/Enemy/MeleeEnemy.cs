using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


public class MeleeEnemy : BaseEnemyClass
{
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;


    [Header("Attack")]
    [Tooltip("The time it takes before the enemy can attack again")]
    public float AttackRate;
    [Tooltip("How far the enemy dashes")]
    public float dashDistance;
    [Tooltip("Controls how fast in seconds it takes the enemy to dash across that distance")]
    public float dashTime;
    [Tooltip("The Damage done to the player every time they get hit")]
    public float damage;
    [Tooltip("The higher the value larger the AOE indicated by the red circle")]
    public float attackAOE;
    [Tooltip("Sprite used for the spin attack")]
    public SpriteRenderer spinAttackSprite;
    [Tooltip("Spin Attack Sprite Spinning speed (revolutions per second)")]
    public float maxSpinSpeed;

    [Header("DEBUG")]
    public float distanceToPlayer1;
    public float distanceToPlayer2;
    public float distanceToTarget;
    public float timeToFire;


    [SerializeField] private GameObject[] players;
    public Transform currentTarget;
    [SerializeField] private bool isSpinAttacking = false;    
    private float actualDashSpeed = 0;
    private Rigidbody2D rb2d;
    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        spinAttackSprite.enabled = false;
        actualDashSpeed = dashDistance/dashTime;
    }


    private void Update()
    {
    }

    // calculates and set target to the closest player to the enemy
    public void TargetClosestPlayer()
    {
        distanceToPlayer1 = Vector2.Distance(players[0].transform.position, transform.position);
        distanceToPlayer2 = Vector2.Distance(players[1].transform.position, transform.position);
        if (distanceToPlayer1 < distanceToPlayer2)
        {
            currentTarget = players[0].transform;
            distanceToTarget = distanceToPlayer1;
        }
        else
        {
            currentTarget = players[1].transform;
            distanceToTarget = distanceToPlayer2;
        }
    }

    public void Attack()
    {

        isSpinAttacking = true;
        StartCoroutine(Attacking());
        // if (Vector2.Distance(currentTarget.transform.position, transform.position) < attackAOE) 
        // {
        //     rb2d.velocity = (currentTarget.position - transform.position).normalized * speedWhileAttacking;
        //     if (currentTarget.gameObject.name == "Warden")
        //     {
        //         EventManager.instance.playerEvents.PlayerDamage(damage, "Warden");
        //     }
        //     else if (currentTarget.gameObject.name == "Gatherer")
        //     {
        //         EventManager.instance.playerEvents.PlayerDamage(damage, "Gatherer");
        //     }
        // }
    }

    // this is for a better lerp function
    // referenced from this video: https://www.youtube.com/watch?v=LSNQuFEDOyQ&ab_channel=FreyaHolm%C3%A9r
    private Vector2 expDecay(Vector2 a, Vector2 b, float decay) {
        return b+(a-b)*MathF.Exp(-decay*Time.deltaTime);
    }
    IEnumerator Attacking()
    {
        Debug.Log("Entering Spin Attack");
        //Debug.Break();
        spinAttackSprite.enabled = true;
        Vector3 directionToTarget = (currentTarget.position - transform.position).normalized;
        Vector2 dashVector = directionToTarget * actualDashSpeed;
        float attackStartTime = Time.time;
        // Attack Proper
        while (Time.time - attackStartTime < dashTime) {
            rb2d.AddForce(dashVector,ForceMode2D.Force);
            rb2d.velocity = dashVector;
            spinAttackSprite.transform.Rotate(Vector3.forward*maxSpinSpeed);
            if (Vector2.Distance(currentTarget.transform.position, transform.position) < attackAOE) 
            {
                if (currentTarget.gameObject.name == "Warden")
                {
                    EventManager.instance.playerEvents.PlayerDamage(damage, "Warden");
                }
                else if (currentTarget.gameObject.name == "Gatherer")
                {
                    EventManager.instance.playerEvents.PlayerDamage(damage, "Gatherer");
                }
            }
            yield return new WaitForFixedUpdate();
        }
        // Attack Winddown

        isSpinAttacking = false;
        spinAttackSprite.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackAOE);
    }

    public void AggroMove() {
        Vector3 direction = currentTarget.position - transform.position;
        rb2d.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;
    }
}
