using System.Collections;
using UnityEngine;

// Jim Lee <-- who to ask and blame if something here doesn't work

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _lifetime;
    private float _damage;
    private float _AOESize;
    private float _AOElifetime;
    [HideInInspector] public float _AOEdamage;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject AOEPrefab;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // buncha math and stuff to make the projectile move towards the target and angled correctly
    // I'll explain it if needed but eugh - Jim Lee
    public void InitializeProjectile(Vector3 targetPosition, float offset, float projectileSpeed, float lifetime, float damage, float AOESize, float AOElifetime, float AOEDamage)
    {
        projectilePrefab.SetActive(true);
        AOEPrefab.SetActive(false);
        _lifetime = lifetime;
        _damage = damage;
        _AOESize = AOESize;
        _AOElifetime = AOElifetime;
        _AOEdamage = AOEDamage;
        Vector3 direction = targetPosition - transform.position;
        direction = Quaternion.Euler(0, 0, offset) * direction;

        Vector3 rotation = transform.position - targetPosition;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90 + offset);

        rb.velocity = new Vector2(direction.x, direction.y).normalized * projectileSpeed;
        StartCoroutine(DespawnProjectile());
    }

    // on hitting a Tag that isn't enemy
    // instatiates(from pool) the AOE prefab and deactivates the projectile
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            AOEPrefab.SetActive(true);
            AOEPrefab.transform.localScale = Vector3.one * _AOESize;
            rb.velocity = Vector3.zero;
            if (collision.CompareTag("Player"))
            {
                // TODO: DEAL DAMAGE TO PLAYER
            }
            projectilePrefab.SetActive(false);
            StartCoroutine(DespawnAOE());
        }
    }

    // used to expire the projectile if it doesn't hit anything
    IEnumerator DespawnProjectile()
    {
        // spawns AOE prefab AFTER the main projectile expires
        yield return new WaitForSeconds(_lifetime);
        AOEPrefab.SetActive(true);
        AOEPrefab.transform.localScale = Vector3.one * _AOESize;
        rb.velocity = Vector3.zero;
        projectilePrefab.SetActive(false);
        StartCoroutine(DespawnAOE());
    }

    IEnumerator DespawnAOE()
    {
        yield return new WaitForSeconds(_AOElifetime);
        AOEPrefab.SetActive(false);
        gameObject.SetActive(false);
    }
}
