using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _AOESize;
    private float _lifetime;
    private float _AOElifetime;
    [SerializeField] private GameObject AOEPrefab;
    


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // buncha math and stuff to make the projectile move towards the target and angled correctly
    // I'll explain it if needed but eugh - Jim Lee
    public void InitializeProjectile(Vector3 targetPosition, float offset, float projectileSpeed, float lifetime, float AOESize, float AOElifetime)
    {
        _AOESize = AOESize;
        _lifetime = lifetime;
        _AOElifetime = AOElifetime;
        Vector3 direction = targetPosition - transform.position;
        direction = Quaternion.Euler(0, 0, offset) * direction;

        Vector3 rotation = transform.position - targetPosition;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90 + offset);

        rb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;
        StartCoroutine(Despawn());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            Instantiate(AOEPrefab, transform.position, Quaternion.identity).GetComponent<AOE>().InitializeAOE(_AOESize, _AOElifetime);
            Destroy(gameObject);
        }
    }

    // TODO: use pooling instead
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(_lifetime);
        Instantiate(AOEPrefab, transform.position, Quaternion.identity).GetComponent<AOE>().InitializeAOE(_AOESize, _AOElifetime);
        Destroy(gameObject);
    }

}
