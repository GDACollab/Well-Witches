using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossBush : MonoBehaviour, IInteractable
{
    private ParticleSystem ps;
    private Light2D light2d;
    public bool interacted;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        light2d = GetComponentInChildren<Light2D>();
    }
    private void Start()
    {
        interacted = false;
    }

    private void OnEnable()
    {
        EventManager.instance.bossEvents.onBushReset += BossBushReset;
    }
    private void OnDisable()
    {
        EventManager.instance.bossEvents.onBushReset -= BossBushReset;
    }

    public void BossBushReset()
    {
        ps.Play();
        interacted = false;
        light2d.enabled = true;
    }

    private void DisableBush()
    {
        ps.Stop();
        light2d.enabled = false;
        interacted = true;
    }

    void IInteractable.Interact()
    {
        EventManager.instance.bossEvents.BushCollected();
        DisableBush();
    }

}
