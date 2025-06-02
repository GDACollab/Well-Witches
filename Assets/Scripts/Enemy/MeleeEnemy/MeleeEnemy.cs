using UnityEngine;
using System.Collections;

public class MeleeEnemy : BaseEnemyClass
{
    private float damage;
    private float attackAOE;
    private float speedWhileAttacking;

    //public Animator animator;
    public SpriteRenderer atkSprite;
    public Transform atkSpritePos;
    public float atkSpeed = 10;     // Speed of animation
    public float atkDuration = 1;   // Length of animation

    private void Start()
    {
        stats = EnemySpawner.Instance.difficultyStats[EnemySpawner.Instance.currentDifficulty];
        health = stats.meleeHealth;
        moveSpeed = stats.meleeSpeed;
        range = stats.meleeRange;
        stunDuration = stats.stunDuration;

        damage = stats.meleeDamage;
        timeBetweenAttack = stats.meleeTimeBetweeAttacks;
        attackAOE = stats.meleeAttackAOE;
        speedWhileAttacking = stats.meleeSpeedWhileAttacking;
        atkSprite.enabled = false;
    }

    public override void Attack()
    {
        agent.speed = speedWhileAttacking;
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.bruiserAttackDash, this.transform.position);

        // not very performantive, better if collider check but should be good enough
        if (Vector2.Distance(transform.position, currentTarget.position) <= attackAOE)
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

        StartCoroutine(AttackAnimation());

    }

    IEnumerator AttackAnimation()
    {
        float timeElapsed = 0;
        atkSprite.enabled = true;

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.bruiserAttackSwipe, this.transform.position);

        while (timeElapsed < atkDuration)
        {
            atkSpritePos.Rotate(new Vector3(0, 0, atkSpeed) * Time.deltaTime);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        atkSpritePos.rotation = Quaternion.Euler(0f, 0f, 0f);
        atkSprite.enabled = false;

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackAOE);
    }
#endif
}
