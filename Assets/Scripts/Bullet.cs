using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 mousePosition;
    private Camera cam;
    private Rigidbody2D rb;
    public float startingVelocity;
    public float acceleration;
    public float maxVelocity;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * startingVelocity;  
        
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        StartCoroutine(Despawn());

    }

    // Update is called once per frame
    private void Update()
    {
        if (rb.velocity.magnitude < maxVelocity)
        {
            rb.velocity *= acceleration;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }


    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
