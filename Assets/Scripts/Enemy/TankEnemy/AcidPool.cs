using System.Collections;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    private float lifetime;
    private float damage;

    [SerializeField] ParticleSystem pool;
    ParticleSystem.MainModule poolMainModule;

    private float damageTick;
    private float timer = 0f;

    private EnemyStatsSO stats;

    public void InitializeAcid()
    {
        stats = EnemySpawner.Instance.difficultyStats[EnemySpawner.Instance.currentDifficulty];
        poolMainModule = pool.main;

        lifetime = stats.tankAcidLifetime;
        damage = stats.tankAcidDamage;
        damageTick = stats.tankAcidTick;

        // need subtract a sec to get the fading effect
        poolMainModule.startLifetime = lifetime-1f;


        StartCoroutine(DespawnAcid());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            
            case "Warden":
                EventManager.instance.playerEvents.PlayerDamage(damage, "Warden");
                break;
            case "Gatherer":
                EventManager.instance.playerEvents.PlayerDamage(damage, "Gatherer");
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (timer >= damageTick)
        {
            timer = 0f;
            switch (collision.gameObject.name)
            {
                case "Warden":
                    EventManager.instance.playerEvents.PlayerDamage(damage, "Warden");
                    break;
                case "Gatherer":
                    EventManager.instance.playerEvents.PlayerDamage(damage, "Gatherer");
                    break;
                default:
                    break;
            }
        } else
        {
            timer += Time.fixedDeltaTime;
        }
    }

    // acid pool should go away after a certain amount of time
    IEnumerator DespawnAcid()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}
