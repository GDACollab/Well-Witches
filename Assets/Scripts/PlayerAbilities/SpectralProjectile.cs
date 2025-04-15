using UnityEngine;

public class SpectralProjectile : MonoBehaviour
{
    [Header("Debug, do not change")]
    [SerializeField] private Transform target;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    private bool once = true;
    private Vector3 relativeDistance = Vector3.zero;
    public void Initialize(Transform pivot, float damage, float speed, float distance, float lifetime)
    {
        target = pivot;
        _damage = damage;
        _speed = speed;
        _distance = distance;
        transform.SetParent(pivot);
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        //target.Rotate(Vector3.forward, _speed);
        //transform.position = (target.position + relativeDistance);
        //gameObject.transform.RotateAround(target.position, Vector3.forward, _speed * Time.deltaTime);
        //if (once)
        //{
        //    transform.position *= (_distance / 1000);
        //    once = false;
        //}
        //relativeDistance = transform.position - target.position;

    }
}
