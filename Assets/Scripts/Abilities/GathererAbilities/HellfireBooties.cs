using System.Collections;
using UnityEngine;

public class HellfireBooties : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    private float duration;
    private float timeInBetweenTicks;


    public void Initialize(float duration, float damage, float flameTicksPerSecond)
    {
        this.damage = damage;
        this.duration = duration;
        timeInBetweenTicks = 1f / flameTicksPerSecond;
        Destroy(gameObject, this.duration);
    }


    //if collision with enemy is detected it starts the dealDamage Coroutine
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.layer == 6) {
            StartCoroutine(DealDamage(collision.GetComponent<BaseEnemyClass>()));
        }
    }
    //stops coroutine when monster leaves flames.
    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.layer == 6)
        {
            StopCoroutine(DealDamage(hit.GetComponent<BaseEnemyClass>()));
        }
    }

    //deals damage to enemy
    IEnumerator DealDamage(BaseEnemyClass enemy) {
        Debug.Log("taken damage from hellfire");
        enemy.TakeDamage(damage);
        yield return new WaitForSeconds(timeInBetweenTicks);
        yield return null;
    }
}
