using System.Collections.Generic;
using UnityEngine;

public class GourdForge : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private float damagePerTick;
    [SerializeField] private float damageTickDuration;
    [SerializeField] private float damageTickCounter = 0;

    [SerializeField] List<Collider2D> enemiesInAOE = new List<Collider2D>();
    [SerializeField] ContactFilter2D enemyFilter = new ContactFilter2D();
    Warden_Movement wardenRef;

    public void Activate(float damagePerTick, float damageTickDuration, float size, float lifespan)
    {
        Debug.Log("ACTIVATING GOURD FORGE");

        this.damagePerTick = damagePerTick;
        this.damageTickDuration = damageTickDuration;

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.35f);
        transform.localScale = Vector3.one * size;
        wardenRef = GetComponentInParent<Warden_Movement>();
        wardenRef.canMove = false;
        Destroy(gameObject, lifespan);
    }

    private void Update()
    {
        HandleDamageTick();
    }
    void OnDestroy(){
        Debug.Log("DESTROYED!!!");
        wardenRef.canMove = true;
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
        int hitEnemies = GetComponent<PolygonCollider2D>().OverlapCollider(enemyFilter,enemiesInAOE);
        

        foreach (Collider2D hit in enemiesInAOE) {
            BaseEnemyClass hitEnemy = hit.GetComponent<BaseEnemyClass>();
            if (hitEnemy != null) hitEnemy.TakeDamage(damagePerTick);
        }
        enemiesInAOE.Clear();

    }
}
