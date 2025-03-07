using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private EventInstance currentBGM;

    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    private void Awake()
    {
        eventInstances = new List<EventInstance>();

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        // sound -> the FMOD event you want to play
        // worldPos -> the position in the scene you want the event to play from

        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventRefrence)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventRefrence);
        eventInstances.Add(eventInstance); 
        return eventInstance;
    }

    public void PlayOST(EventReference bgmToPlay)
    {
        //CleanUp();

        if (currentBGM.isValid())
        {
            currentBGM.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            currentBGM.release();
        }
        currentBGM = RuntimeManager.CreateInstance(bgmToPlay);
        currentBGM.start();
    }

    public void CleanUp()
    {
        Debug.Log("Sound EventInstance CleanUp");
        Debug.Log(eventInstances.Count);
        foreach (EventInstance eventInstance in eventInstances)
        {
            Debug.Log("hello");
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        eventInstances.Clear();
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
