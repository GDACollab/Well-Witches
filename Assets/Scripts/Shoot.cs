using System.Collections.Generic;
using UnityEngine;



public class Shoot : MonoBehaviour
{
    [SerializeField] private List<GameObject> bulletList;
    [SerializeField] private int currentNum = 0;

    public float fireRate = 0.5f;

    public float time = 0f;

    private GameObject currentBullet;
    private void Start()
    {
        currentBullet = bulletList[currentNum];
    }

    private void Update()
    {
        time += Time.deltaTime; 
        if (Input.GetMouseButton(0) && time > fireRate)
        {
            Instantiate(currentBullet, transform.position, Quaternion.identity);
            time = 0f;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (currentNum < bulletList.Count -1)
            {
                currentNum++;
            } else
            {
                currentNum = 0;
            }
            currentBullet = bulletList[currentNum];
        }
    }
}
