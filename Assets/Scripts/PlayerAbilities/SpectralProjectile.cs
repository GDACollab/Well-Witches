using UnityEngine;

public class SpectralProjectile : MonoBehaviour
{
    public Transform player;

    public float _damage;



    private void Start()
    {
        player = GameObject.Find("Warden").transform;
    }

    private void Update()
    {
        gameObject.transform.RotateAround(player.position, Vector3.forward, 100 * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, player.position, -(10 - Vector3.Distance(transform.position, player.position)));
    }
}
