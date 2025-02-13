using UnityEngine;

public class WardenShoot : MonoBehaviour
{

    public GameObject projectilePrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }
}

