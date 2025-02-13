using UnityEngine;

public class WardenShoot : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Transform projectileSpawn;

    private void Awake()
    {
        if (projectileSpawn != null) 
        {
            projectileSpawn = transform.Find("ProjectileSpawn");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
        }
    }
}

