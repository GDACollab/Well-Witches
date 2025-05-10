using System.Collections.Generic;
using UnityEngine;

public class AcidTrailPooling : MonoBehaviour
{
    public static AcidTrailPooling SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject acidPoolPrefab;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(acidPoolPrefab);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetProjectileObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
