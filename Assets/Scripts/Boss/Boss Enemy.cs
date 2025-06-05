using UnityEngine;

public class BossEnemy : BaseEnemyClass
{
    [Header("BOSS INFO")]
    public float attackCooldown;
    public bool DPS_phase = false;

    [Header("BOSS References")]
    public Animator animator;
    public GameObject bossShield;

    private void Start()
    {
        currentTarget = GameObject.Find("Gatherer").transform;
    }

    public override void Attack()
    {
        return;
    }

    public override void TakeDamage(float amount, bool fromWardenProjectile = false)
    {

        //Reduces health by the amount entered in Unity, or by 5% of that health outside of DPS phase

        if (DPS_phase)
        {
            health -= amount;
        }
        else
        {
            health -= amount * 0.05f;
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
        SceneHandler.Instance.ToEndingCutscene();
    }
}