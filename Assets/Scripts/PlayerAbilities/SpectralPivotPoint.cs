using UnityEngine;

public class SpectralPivotPoint : MonoBehaviour
{
    [SerializeField] private Transform player;
    private void Update()
    {
        transform.position = player.position;
    }
}
