using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // singleton class to handle events
    public static EventManager instance { get; private set; }

    // event types
    // EX: public PlayerEvents playerEvents;

    private void Awake()
    {
        if(instance == null)
        {
            Debug.LogError("Found more than one GameManager in the scene. Please make sure there is only one");
        }
        instance = this;

        // initialize all events 
        // EX: playerEvents = new PlayerEvents();
    }


}
