using UnityEngine;

public class AOE : MonoBehaviour
{
    private float damage;

    private void Start()
    {
        damage = GetComponentInParent<Projectile>()._AOEdamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // DEAL DAMAGE TO PLAYER
        }
    }
}
