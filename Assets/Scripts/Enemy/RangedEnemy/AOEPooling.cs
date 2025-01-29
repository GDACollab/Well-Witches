using System.Collections.Generic;
using UnityEngine;

// https://learn.unity.com/tutorial/introduction-to-object-pooling#
public class AOEPooling : MonoBehaviour
{
    public static AOEPooling SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject AOEPrefab;
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
            tmp = Instantiate(AOEPrefab);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetAOEObject()
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
