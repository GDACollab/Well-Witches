using UnityEngine;

public class SpectralPivotPoint : MonoBehaviour
{
    [Tooltip("Target should be Warden.")]
    [SerializeField] private Transform target;

    private void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Warden").GetComponent<Transform>();   
        }
    }
    private void Update()
    {
        transform.position = target.position;
    }
}
