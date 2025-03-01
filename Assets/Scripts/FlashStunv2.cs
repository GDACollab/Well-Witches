using System.Collections;
using UnityEngine;

public class FlashStunv2 : MonoBehaviour
{
    public GameObject Light2D;
    public GameObject GlobalVolume;
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
        Light2D.SetActive(false);
        GlobalVolume.SetActive(false);
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
        Light2D.SetActive(true);
        GlobalVolume.SetActive(true);
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(.1f);
        Light2D.SetActive(false);
        GlobalVolume.SetActive(false);
        yield return new WaitForSeconds(1.5f - .1f);
        gameObject.transform.localPosition = Vector2.zero;
        gameObject.SetActive(false);
    }


}
