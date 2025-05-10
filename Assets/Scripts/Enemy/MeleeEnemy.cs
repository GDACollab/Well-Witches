using System;
using System.Collections;
using System.Collections.Generic;
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
    public float distanceToGatherer;
    public float distanceToWarden;
    public float distanceToTarget;


    //[SerializeField] private GameObject[] players;
    [SerializeField] private Dictionary<string, GameObject> players;
    public Transform currentTarget;
    [SerializeField] private bool isSpinAttacking = false;    
    private float actualDashSpeed = 0;
    private Rigidbody2D rb2d;

    private void Start()
    {
        players = GetPlayers();
        rb2d = GetComponent<Rigidbody2D>();
        spinAttackSprite.enabled = false;
        actualDashSpeed = dashDistance/dashTime;
    }
    private Dictionary<string, GameObject> GetPlayers() {
        GameObject[] playerTags = GameObject.FindGameObjectsWithTag("Player");
        Dictionary<string, GameObject> playersDict = new Dictionary<string, GameObject>();
        foreach(GameObject playerTagged in playerTags) {
            if (playerTagged.gameObject.name == "Gatherer") {
                playersDict.Add("Gatherer", playerTagged);
            }
            if (playerTagged.gameObject.name == "Warden") {
                playersDict.Add("Warden", playerTagged);
            }
            if (playersDict.Count == 2) {
                break;
            }
        }
        return playersDict;
    }


    private void Update()
    {
    }

    // calculates and set target to the closest player to the enemy
    public void TargetClosestPlayer()
    {
        distanceToGatherer = Vector2.Distance(players["Gatherer"].transform.position, transform.position);
        distanceToWarden = Vector2.Distance(players["Warden"].transform.position, transform.position);
        if (distanceToGatherer < distanceToWarden)
        {
            currentTarget = players["Gatherer"].transform;
            distanceToTarget = distanceToGatherer;
        }
        else
        {
            currentTarget = players["Warden"].transform;
            distanceToTarget = distanceToWarden;
        }
    }
    public void TargetGathererPlayer() {
        currentTarget = players["Gatherer"].transform;
        distanceToGatherer = Vector2.Distance(players["Gatherer"].transform.position, transform.position);
        distanceToTarget = distanceToGatherer;
    }

    public void Attack()
    {
        isSpinAttacking = true;
        StartCoroutine(Attacking());
    }
    IEnumerator Attacking()
    {
        Debug.Log("Entering Spin Attack");
        //Debug.Break();
        spinAttackSprite.enabled = true;
        Vector3 directionToTarget = (currentTarget.position - transform.position).normalized;
        Vector2 dashVector = directionToTarget * actualDashSpeed;
        float attackStartTime = Time.time;
        bool successHit = false;
        // Attack Proper
        while (Time.time - attackStartTime < dashTime) {
            //rb2d.AddForce(dashVector,ForceMode2D.Force);
            if (!successHit) {
                rb2d.velocity = dashVector;
                spinAttackSprite.transform.Rotate(Vector3.forward*maxSpinSpeed);
                if (Vector2.Distance(players["Gatherer"].transform.position, transform.position) < attackAOE) 
                {
                    EventManager.instance.playerEvents.PlayerDamage(damage, "Gatherer");
                    successHit = true;
                }
                if (Vector2.Distance(players["Warden"].transform.position, transform.position) < attackAOE) 
                {
                    EventManager.instance.playerEvents.PlayerDamage(damage, "Warden");
                    successHit = true;
                }
                if (successHit) {
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(-dashVector,ForceMode2D.Impulse);
                    isSpinAttacking = false;
                    spinAttackSprite.enabled = false;
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
        //rb2d.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;

        float distanceToTarget = Vector2.Distance(currentTarget.transform.position, transform.position);
        if (distanceToTarget < moveSpeed)
        {
            // speed starts to scale from distance to the target once the distance becomes less than the max move speed
            // likely needs more fine tuning
            rb2d.velocity = new Vector2(direction.x, direction.y).normalized * (distanceToTarget - (range - 1));
        }
        else
        {
            rb2d.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;
        }
    }
}
