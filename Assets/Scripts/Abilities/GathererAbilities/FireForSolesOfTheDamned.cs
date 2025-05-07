using System.Collections;
using UnityEngine;

public class FireForSolesOfTheDamned : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    private float duration;
    private float timeInBetweenTicks;

    private SolesOfTheDamned SolesOfTheDamned;

    void Start()
    {
        SolesOfTheDamned = GetComponentInParent<SolesOfTheDamned>();
        transform.parent = null;
        damage = SolesOfTheDamned.damage;
        duration = SolesOfTheDamned.duration;
        timeInBetweenTicks = 1f/SolesOfTheDamned.flameTicksPerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        //if the duration is up delete gameobject
        duration -= Time.deltaTime;
        if (duration <= 0f) {
            Destroy(gameObject);
        }
    }

    //if collision with enemy is detected it starts the dealDamage Coroutine
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.layer == 6) {
            StartCoroutine(DealDamage(hit.GetComponent<BaseEnemyClass>()));
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
