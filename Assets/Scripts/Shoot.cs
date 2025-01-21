using System.Collections.Generic;
using UnityEngine;



public class Shoot : MonoBehaviour
{
    [SerializeField] private List<GameObject> bulletList;
    [SerializeField] private int currentNum = 0;

    private GameObject currentBullet;
    private void Start()
    {
        currentBullet = bulletList[currentNum];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(currentBullet, transform.position, Quaternion.identity);
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
