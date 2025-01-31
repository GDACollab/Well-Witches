using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _AOESize;
    private float _lifetime;
    private float _AOElifetime;
    private float _damage;
    private float _AOEdamage;
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
        StartCoroutine(Despawn());
    }

    // on hitting a Tag that isn't enemy
    // instatiates(from pool) the AOE prefab and deactivates the projectile
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            // spawns the AOE prefab
            GameObject AOE = AOEPooling.SharedInstance.GetAOEObject();
            if (AOE != null) 
            {
                AOE.transform.position = transform.position;
                AOE.transform.localScale = Vector3.one * _AOESize;
                AOE.SetActive(true);
                AOE.GetComponent<AOE>().DespawnAOE(_AOElifetime, _AOEdamage);
            }
            
            // TODO: DAMAGE
            // use _damage variable

            gameObject.SetActive(false);    
        }
    }

    // used to expire the projectile if it doesn't hit anything
    IEnumerator Despawn()
    {
        // spawns AOE prefab AFTER the main projectile expires
        yield return new WaitForSeconds(_lifetime);
        GameObject AOE = AOEPooling.SharedInstance.GetAOEObject();
        if (AOE != null)
        {
            AOE.transform.position = transform.position;
            AOE.transform.localScale = Vector3.one * _AOESize;
            AOE.SetActive(true);
            AOE.GetComponent<AOE>().DespawnAOE(_AOElifetime, _AOEdamage);
        }
        gameObject.SetActive(false);
    }
}
