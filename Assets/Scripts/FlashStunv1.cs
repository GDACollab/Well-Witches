using System.Collections;
using UnityEngine;

public class FlashStunv1 : MonoBehaviour
{
    public ParticleSystem particles;
    

    public float startingVelocity;
    public float lifetime;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
        gameObject.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        var main = particles.main;  
        main.startSpeed = Map(rb.velocity.y, -startingVelocity, startingVelocity, -4f, 4f);
    }

    public void LaunchStun()
    {
        gameObject.SetActive(true);

        rb.gravityScale = 1;
        rb.velocity = new Vector3(0, 1, 0) * startingVelocity;
        StartCoroutine(Explode());
    }

    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (toMax - toMin) * ((value - fromMin) / (fromMax - fromMin)) + toMin;
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(lifetime);
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(3);
        gameObject.transform.localPosition = Vector2.zero;
        gameObject.SetActive(false);
    }


}
