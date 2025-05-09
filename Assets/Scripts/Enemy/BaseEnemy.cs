using System;
using System.Collections;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemyClass : MonoBehaviour
{
    [Header("Enemy Stats")]
    [Tooltip("The max health of an enemy.")]
    public float health;
    [Tooltip("How fast an enemy moves.")]
    public float moveSpeed;
    [Range(0, 20)]
    [Tooltip("How far away the enemy stops before attacking")]
    public float range;
    public SpriteRenderer sr;

    public NavMeshAgent agent;

    public Rigidbody2D rb;
    SiphonEnergy siphon;
    public void Spawn(Vector3 position)
    {
        Instantiate(gameObject, position, Quaternion.identity);
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
        if (WardenAbilityManager.Instance.passiveAbilityName == "WaterLogging") {
            StartCoroutine(slow());
        }
    }

    public virtual void Die()
    {
        EnemySpawner.currentEnemyCount--;
        //if siphon energy is equipped then add to siphone times
        if (WardenAbilityManager.Instance.passiveAbilityName == "SiphonEnergy")
        {
            WardenAbilityManager.Instance.siphonTimes++;
        }
        Destroy(gameObject);
    }

    public virtual void ProjectileKnockback(Vector3 force)
    {
        agent.enabled = false;
        rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(EnableAgent());
    }
    
    IEnumerator EnableAgent()
    {
        yield return new WaitForSeconds(0.2f);
        agent.enabled = true;
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector3.zero;
    }

    IEnumerator slow() {
        Debug.Log("SLOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOW");
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.blue;
        moveSpeed = moveSpeed * 0.8f;
        yield return new WaitForSeconds(WardenAbilityManager.Instance.waterDuration);
        moveSpeed = moveSpeed / 0.8f;
        sr.color = Color.white;
    }
}
