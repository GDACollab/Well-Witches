using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(Collider2D))]
public class ColliderDamageTrigger : MonoBehaviour
{
    [SerializeField] BossProjectile projectile;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (collision.gameObject.name)
            {
                case "Warden":
                    EventManager.instance.playerEvents.PlayerDamage(projectile.damage, "Warden");
                    break;
                case "Gatherer":
                    EventManager.instance.playerEvents.PlayerDamage(projectile.damage, "Gatherer");
                    break;
                default:
                    break;
            }
        }
    }
}
