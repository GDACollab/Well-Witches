using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashStun : MonoBehaviour
{
    public Light2D Light2D;
    public ParticleSystem particles;
    

    float startingVelocity;
    float lifetime;
    float flashDuration;

    private Rigidbody2D rb;

    private void Update()
    {
        var main = particles.main;  
        main.startSpeed = Map(rb.velocity.y, -startingVelocity, startingVelocity, -4f, 4f);
    }

    public void Initialize(float startingVelocity, float flashDuration, float lifetime)
    {
        this.startingVelocity = startingVelocity;
        this.flashDuration = flashDuration;
        this.lifetime = lifetime;
        gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        Light2D.gameObject.SetActive(false);
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
        Light2D.gameObject.SetActive(true);
        Light2D.intensity = 10f;
        float timeElapsed = 0f;
        while (timeElapsed < flashDuration && Light2D.intensity > 0f)
        {
            float t = timeElapsed / flashDuration;
            Light2D.intensity -= t;
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        gameObject.transform.localPosition = Vector2.zero;
        Destroy(gameObject);
    }


}
