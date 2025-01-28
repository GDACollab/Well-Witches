using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PrefabSpawner : MonoBehaviour
{
    public static PrefabSpawner Spawner {get; private set;}

    [SerializeField] private List<PrefabEntry> prefabEntries; // List to configure prefabs in the Inspector

    private Dictionary<string, GameObject> prefabDictionary;

    private void Awake()
    {
        // Awake runs once, initlizes and setups the prefabs to be ready to instantiate
        //If 1st time running this script, do everything
        if (Spawner == null)
        {
            Spawner = this;
            //Reads the list and inputs it into a dictionary
            prefabDictionary = new Dictionary<string, GameObject>();
            foreach (var entry in prefabEntries)
            {
                if (!prefabDictionary.ContainsKey(entry.key))
                {
                    prefabDictionary.Add(entry.key, entry.prefab);
                }
                else
                {
                    Debug.Log("There was a dup key"); 
                }
            }
        }
        //Only here to ensure no other gameobjects have this script
        //If there is another, destroy that gameobject
        else
        {
            Debug.Log("Another gameobject had the PrefabSpawners script");
            Destroy(gameObject); // Ensure there's only one instance
        }
    }

    public GameObject SpawnPrefab(string key, Vector3 position)
    {

        //First checks if the key and prefab actually exists
        //If it does spawn it at the desired location
        if (prefabDictionary.TryGetValue(key, out var prefab))
        {
         
            return Instantiate(prefab, position, Quaternion.identity); //No rotation
        }
        else
        {
            Debug.Log("Could not find the prefab");
            return null;
        }
    }

    [System.Serializable] private class PrefabEntry
    {
        public string key;
        public GameObject prefab;
    }
}
