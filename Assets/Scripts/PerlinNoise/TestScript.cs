using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SpawnPrefab(string key, Vector3 position)
        PrefabSpawner.Spawner.SpawnPrefab("cube", new Vector3(0, 0, 0));
        PrefabSpawner.Spawner.SpawnPrefab("cube", new Vector3(5, 3, 0));
        PrefabSpawner.Spawner.SpawnPrefab("cube", new Vector3(-2, -1, 0));
    }

    // Update is called once per frame

}
