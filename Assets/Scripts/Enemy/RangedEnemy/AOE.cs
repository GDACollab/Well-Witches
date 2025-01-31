using System.Collections;
using UnityEngine;

public class AOE : MonoBehaviour
{
    private float _lifetime;
    private float _damage;
    
    // deactivates AOE after lifetime
    public void DespawnAOE(float lifetime, float damage)
    {
        _lifetime = lifetime;
        _damage = damage;
        StartCoroutine(DeactivateAOE());
    }

    IEnumerator DeactivateAOE()
    {
        yield return new WaitForSeconds(_lifetime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //TODO: DAMAGE
            //use _damage variable
        }
    }

}
