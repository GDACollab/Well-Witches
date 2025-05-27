using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingWardenTakeDamage : MonoBehaviour
{
    [SerializeField] private EventManager playerEvents;


    private void Start()
    {
        Debug.Log("WARNING: REMOVE THIS CHEAT SCRIPT FROM SCENE");
    }

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            playerEvents.playerEvents.PlayerDamage(10, "Warden");
            Debug.Log("Warden take damage");
        }
    }
}
