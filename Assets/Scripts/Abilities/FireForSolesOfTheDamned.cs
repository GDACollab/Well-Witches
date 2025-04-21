using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireForSolesOfTheDamned : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float damage;
    [SerializeField]
    public float duration;
    [SerializeField]
    public float timeInBetweenTicks;

    public SolesOfTheDamned SolesOfTheDamned;

    void Start()
    {
        SolesOfTheDamned = GetComponentInParent<SolesOfTheDamned>();
        transform.parent = null;
        duration = SolesOfTheDamned.duration;
        damage = SolesOfTheDamned.damage;
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
            StartCoroutine(dealDamage(hit.GetComponent<BaseEnemyClass>()));
        }
    }
    //stops coroutine when monster leaves flames.
    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.layer == 6)
        {
            StopCoroutine(dealDamage(hit.GetComponent<BaseEnemyClass>()));
        }
    }

    //deals damage to enemy
    IEnumerator dealDamage(BaseEnemyClass enemy) {
        Debug.Log("taken damage");
        enemy.TakeDamage(damage);
        yield return new WaitForSeconds(timeInBetweenTicks);
        yield return null;
    }
}
