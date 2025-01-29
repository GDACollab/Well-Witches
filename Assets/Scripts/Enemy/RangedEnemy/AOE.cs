using System.Collections;
using UnityEngine;

public class AOE : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;
    private float _lifetime;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }
    
    // deactivates AOE after lifetime
    public void DespawnAOE(float lifetime)
    {
        _lifetime = lifetime;
        StartCoroutine(DeactivateAOE());
    }

    IEnumerator DeactivateAOE()
    {
        yield return new WaitForSeconds(_lifetime);
        gameObject.SetActive(false);
    }
    
}
