using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    public void Activate(float duration)
    {
        Destroy(gameObject, duration);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyProjectile>() != null)
        {
            Rigidbody2D proj = collision.GetComponent<Rigidbody2D>();
            collision.transform.rotation = Quaternion.Euler(collision.transform.rotation.eulerAngles.x, collision.transform.rotation.eulerAngles.y, collision.transform.rotation.eulerAngles.z + 180f);
            proj.velocity = collision.gameObject.transform.up * collision.GetComponent<EnemyProjectile>().speed;
        }
    }


}
