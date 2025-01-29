using System.Collections;
using UnityEngine;

public class AOE : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;
    private float lifetime;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }
    public void InitializeAOE(float radius, float AOElifetime)
    {
        lifetime = AOElifetime;
        circleCollider2D.radius = radius;
        transform.localScale *= radius;
        Destroy(gameObject, lifetime);
    }
}
