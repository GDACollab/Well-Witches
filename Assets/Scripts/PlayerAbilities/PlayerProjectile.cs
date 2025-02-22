using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera cam;
    private Rigidbody2D rb;

    [SerializeField] private ParticleSystem head;
    [SerializeField] private ParticleSystem sparks;
    [SerializeField] private ParticleSystem tails;
    [SerializeField] private ParticleSystem impact;
    [SerializeField] private TrailRenderer trail_VFX;

    private float _damage;

    private void Start()
    {
    }

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

        head.gameObject.SetActive(true);
        sparks.gameObject.SetActive(true);
        tails.gameObject.SetActive(true);
        impact.gameObject.SetActive(false);

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // LET ME EDIT BASE CLASS ENEMY WHY IS IT NOT CAPILIZED AND WHY IS IT INT
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BaseEnemyClass>().takingDamage((int) _damage);
        }  
        rb.velocity = Vector2.zero;
        head.gameObject.SetActive(false);
        sparks.gameObject.SetActive(false);
        tails.gameObject.SetActive(false);
        impact.gameObject.SetActive(true);

        Destroy(gameObject, 0.5f);
    }
}
