using System.Collections;
using UnityEditor;
using UnityEngine;

public class HellfireBooties : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    private float duration;
    private float timeInBetweenTicks;

    [SerializeField] LayerMask enemyMask;
    public void Initialize(float duration, float damage, float flameTicksPerSecond, bool right, float offset)
    {
        this.damage = damage;
        this.duration = duration;
        timeInBetweenTicks = 1f / flameTicksPerSecond;
        Destroy(gameObject, this.duration);

        if (right)
        {
            transform.position += transform.right * offset;
        } else
        {
            transform.position -= transform.right * offset;
        }
    }

    //if collision with enemy is detected it starts the dealDamage Coroutine
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        if ((1 << hit.layer) == enemyMask) {
            StartCoroutine(DealDamage(collision.GetComponent<BaseEnemyClass>()));
        }
    }
    //stops coroutine when monster leaves flames.
    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        if ((1 << hit.layer) == enemyMask)
        {
            StopCoroutine(DealDamage(hit.GetComponent<BaseEnemyClass>()));
        }
    }

    //deals damage to enemy
    IEnumerator DealDamage(BaseEnemyClass enemy) {
        enemy.TakeDamage(damage);
        yield return new WaitForSeconds(timeInBetweenTicks);
        yield return null;
    }
}
