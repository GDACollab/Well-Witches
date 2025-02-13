using System.Collections;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera cam;
    private Rigidbody2D rb;
    public float velocity;

    public GameObject traitless_VFX;
    public GameObject trail_VFX;
    public GameObject impact_VFX;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * velocity;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        traitless_VFX.SetActive(true);
        impact_VFX.SetActive(false);

        StartCoroutine(Despawn());

    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("PlayerProjectile"))
        {
            rb.velocity = Vector2.zero;
            traitless_VFX.SetActive(false);
            trail_VFX.SetActive(false);
            impact_VFX.SetActive(true);

            Destroy(gameObject, 0.5f);
        }
    }


    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
