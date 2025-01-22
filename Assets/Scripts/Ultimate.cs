using UnityEngine;

public class Ultimate : MonoBehaviour
{
    [SerializeField] private ParticleSystem chargingParticles;
    [SerializeField] private ParticleSystem orb;
    [SerializeField] private ParticleSystem shockwave;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            chargingParticles.Play();
            orb.Play();
            shockwave.Play();
        }
    }
}
