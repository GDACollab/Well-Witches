using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _lifetime;
    private float _damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitializeAcid(float lifetime, float damage)
    {
        _lifetime = lifetime;
        _damage = damage;

        StartCoroutine(DespawnAcid());
    }

    // acid pool should go away after a certain amount of time
    IEnumerator DespawnAcid()
    {
        yield return new WaitForSeconds(_lifetime);
        gameObject.SetActive(false);
    }
}
