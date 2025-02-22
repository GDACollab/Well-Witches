using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttack : MonoBehaviour
{
    public float attackCooldown = 2f;
    public float attackTimer;
    public float attackRange = 3f;
    public float lookRadius = 15f;
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
    public void onTriggerEnter2D(Collider2D other){
        IDamageable hit = other.GetComponent<IDamageable>();
        if(hit != null){
            hit.TakeDamage(clawDamage);
            target.Bleeding().applyDamage();
        }
    }
}
