using System.Collections.Generic;
using UnityEngine;

// https://learn.unity.com/tutorial/introduction-to-object-pooling#
public class ProjectilePooling : MonoBehaviour
{
    public static ProjectilePooling SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject projectilePrefab;
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
            tmp = Instantiate(projectilePrefab, gameObject.transform);
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
