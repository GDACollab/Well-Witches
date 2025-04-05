using UnityEngine;
using UnityEngine.VFX;

public class Ultimate : MonoBehaviour
{
    [SerializeField] private ParticleSystem chargingParticles;
    [SerializeField] private ParticleSystem orb;
    [SerializeField] private VisualEffect shockwave;

    public CameraShake cameraShake;
    public GameObject volume;
    public GameObject light2d;

    private void Start()
    {
        volume.SetActive(false);
        light2d.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            chargingParticles.Play();
            orb.Play();
            shockwave.Play();
            StartCoroutine(cameraShake.Shake(.15f, .4f, 1f, volume, light2d));
        }
    }
}
