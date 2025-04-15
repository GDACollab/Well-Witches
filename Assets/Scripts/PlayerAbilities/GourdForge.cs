using System.Collections.Generic;
using UnityEngine;

public class GourdForge : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private float damagePerTick;
    [SerializeField] private float damageTickDuration;
    [SerializeField] private float damageTickCounter = 0;

    HashSet<Collider2D> enemiesInAOE = new HashSet<Collider2D>();

    public void Activate(float damagePerTick, float damageTickDuration, float size, float lifespan)
    {
        this.damagePerTick = damagePerTick;
        this.damageTickDuration = damageTickDuration;

        transform.localScale = Vector3.one * size;
        Destroy(gameObject, lifespan);
    }

    private void Update()
    {
        HandleDamageTick();
    }

    void HandleDamageTick()
    {
        if (damageTickCounter > 0) damageTickCounter -= Time.deltaTime;
        if (damageTickCounter <= 0)
        {
            DamageEnemies();
            damageTickCounter = damageTickDuration;
        }
    }
    void DamageEnemies()
    {
        foreach (Collider2D collider in enemiesInAOE) collider.GetComponent<BaseEnemyClass>().TakeDamage(damagePerTick);
    }
}
