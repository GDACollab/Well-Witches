using System.Collections;
using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    public void Activate(float duration)
    {
        Destroy(gameObject, duration);
    }

    // so there's the problem that it's constantly colliding with the Gatherer
    // this could also be fixed by making a Shield layer, separating the Warden and Gatherer layers
    // then configuring the layer collision matrix so Shield only collide with Warden and Enemies
    // BUT that messes with other layer checking stuff somewhere else so
    // this really ugly code here works - Jim
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "EnemyFireball(Clone)":
                Rigidbody2D proj = collision.GetComponent<Rigidbody2D>();
                collision.transform.rotation = Quaternion.Euler(collision.transform.rotation.eulerAngles.x, collision.transform.rotation.eulerAngles.y, collision.transform.rotation.eulerAngles.z + 180f);
                proj.velocity = collision.gameObject.transform.up * collision.GetComponent<EnemyProjectile>().speed;
                break;
            case "Warden":
                StartCoroutine(PopShield());
                break;
            case "MeleeEnemy(Clone)":
                StartCoroutine(PopShield());
                break;
            case "RangedEnemy(Clone)":
                StartCoroutine(PopShield());
                break;
            case "TankEnemy(Clone)":
                StartCoroutine(PopShield());
                break;
            default:
                break;
        }
        
    }

    IEnumerator PopShield()
    {
        // idk yet
        Destroy(gameObject);
        yield return null;
    }

}
