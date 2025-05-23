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
            tmp = Instantiate(acidPoolPrefab, gameObject.transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetAcidPoolObject()
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
