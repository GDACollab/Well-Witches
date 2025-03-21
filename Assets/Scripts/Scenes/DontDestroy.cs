using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{

    // array to keep all game objects, set to whatever size is needed
    private static GameObject[] persistentObjects = new GameObject[3];
    [Header("Index")]
    [Tooltip("The object index determines what kind of object this is\n0 = Managers\n1 = Canvas\n2 = NPCs")]
    // 0 = Managers
    // 1 = Canvas
    // 2 = NPCs
    //  = Gatherer (not yet implemented)
    //  = Warden (not yet implemented)
    public int objectIndex;

    private void Awake()
    {
        if (persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistentObjects[objectIndex] != gameObject)
        {
            Destroy(gameObject);
        }
    }
}

