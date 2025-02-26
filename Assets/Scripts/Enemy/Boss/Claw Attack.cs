using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttack : MonoBehaviour
{
    // These variables are used in the script to help with the attack and cooldown functions
    public float attackCooldown = 2f;
    public float attackTimer;
    public float dmg;
    private bool canDmg;
    public float attackDuration;
    public float timeSinceAttack;
    public float targetHealth = 100;
    private float bleedingdamagepersecond = 0.5f; 
    
    public float clawDamage = 10f;
    UnityEngine.AI.NavMeshAgent agent;
    Transform target;
    public Transform attachSphereCenter;
    public void Bleeding(){
        Debug.Log("Bleeding");
        targetHealth = targetHealth - bleedingdamagepersecond;
    }
    void Update(){
        timeSinceAttack += Time.deltaTime;
        if (timeSinceAttack >= attackCooldown){
            canDmg = false;
        }
    }
    public void Attack(){
        canDmg = true;
        timeSinceAttack = 0;
    }
    public void StopAttack(){
        canDmg = false;
    }
    void onTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            target = other.transform;
            if (canDmg){
                target.GetComponent<PlayerHealth>().TakeDamage(clawDamage);
                Bleeding();
            }
        }
    }
}
