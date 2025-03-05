using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera cam;
    private Rigidbody2D rb;

    [SerializeField] private List<GameObject> projectileComponents;
    [SerializeField] private GameObject impact;

    private float _damage;

    // Start is called before the first frame update
    public void InitializeProjectile(float velocity, float lifetime, float damage)
    {
        _damage = damage;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * velocity;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);


        foreach(GameObject component in projectileComponents) 
        {
            component.SetActive(true);
        }
        impact.gameObject.SetActive(false);

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BaseEnemyClass>().TakeDamage(_damage);
        }  
        rb.velocity = Vector2.zero;
        foreach (GameObject component in projectileComponents)
        {
            component.SetActive(false);
        }
        impact.gameObject.SetActive(true);

        Destroy(gameObject, 0.5f);
    }
}
