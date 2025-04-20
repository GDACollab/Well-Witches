using System.Collections;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    private float _lifetime;
    private float _damage;
    private float _size;

    [SerializeField] ParticleSystem pool;
    ParticleSystem.MainModule poolMainModule;


    public void InitializeAcid(float lifetime, float damage, float size)
    {
        
        poolMainModule = pool.main;

        _lifetime = lifetime;
        _damage = damage;

        // need subtract a sec to get the fading effect
        poolMainModule.startLifetime = _lifetime-1f;


        StartCoroutine(DespawnAcid());
    }

    // acid pool should go away after a certain amount of time
    IEnumerator DespawnAcid()
    {
        yield return new WaitForSeconds(_lifetime);
        gameObject.SetActive(false);
    }
}
