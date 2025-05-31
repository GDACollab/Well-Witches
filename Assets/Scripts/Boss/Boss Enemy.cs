using UnityEngine;

public class BossEnemy : BaseEnemyClass
{
    [Tooltip("Time Between Attacks")]
    public float attackCooldown;
    [Tooltip("Phase 1 to 2 HP")]
    public float phaseHP;
    public Animator animator;

    public bool DPS_phase = false;
    public GameObject bossShield;

    private void Start()
    {
        currentTarget = GameObject.Find("Gatherer").transform;
    }

    public override void Attack()
    {
        return;
    }

    public void Claw_attack()
    {
        Debug.Log("Claw Attack");
    }
    public void Cape_Swipe()
    {
        Debug.Log("Cape Swipe");
    }
    public void Spawn_Enemies()
    {
        Debug.Log("Spawn Enemies");
    }

    public float shield_damage_scalar = 0.05f;

    public override void TakeDamage(float amount, bool fromWardenProjectile = false)
    {

        //Reduces health by the amount entered in Unity, or by 5% of that health outside of DPS phase

        if (DPS_phase)
        {
            health -= amount;
        }
        else
        {
            health -= amount * shield_damage_scalar;
        }

        if (health <= 0)
        {
            Die();
        }
    }
    public override void Die(bool fromWardenProjectile = false)
    {
        Destroy(gameObject);
        Debug.Log("Boss dead yippee"); //Make boss drop quest item here.
    }
}