using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera cam;
    private Rigidbody2D rb;

    public GameObject main_VFX;
    public GameObject trail_VFX;
    public GameObject impact_VFX;

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

        main_VFX.SetActive(true);
        impact_VFX.SetActive(false);

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // LET ME EDIT BASE CLASS ENEMY
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BaseEnemyClass>().takingDamage((int) _damage);
        }  
        rb.velocity = Vector2.zero;
        main_VFX.SetActive(false);
        trail_VFX.SetActive(false);
        impact_VFX.SetActive(true);

        Destroy(gameObject, 0.5f);
    }
}
