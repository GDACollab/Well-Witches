using System.Collections;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float startingVelocity;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(-1, 0).normalized * startingVelocity;
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
