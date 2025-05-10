using System.Collections;
using UnityEngine;

public class HealthTransfer : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlesExiting;
    [SerializeField] private ParticleSystem particlesEntering;

    ParticleSystem.MainModule delayModule;

    public IEnumerator Initialize(Transform wardenTransform, float delay)
    {
        delayModule = particlesEntering.main;
        delayModule.startDelay = delay;
        yield return new WaitForSeconds(delay);
        transform.parent = wardenTransform;
        transform.localPosition = Vector2.zero;
        Destroy(gameObject, 2);
    }
}
