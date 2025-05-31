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
    private EventInstance currentCharacterTalk;
    private EventInstance gathererHurt;
    private EventInstance wardenHurt;

    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    public Bus MasterBus, MusicBus, SFXBus;

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
        
        MasterBus = RuntimeManager.GetBus("bus:/");
        // UNCOMMENT EVERYTHING BELOW WHEN BUSES ARE CORRECT AND DELETE THIS LINE AND BELOW LINE
        // May need to change the "bus:/Music" and "bus:/SFX" to the correct bus paths
        MusicBus = RuntimeManager.GetBus("bus:/Music");
        SFXBus = RuntimeManager.GetBus("bus:/SFX");
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

    public void PlayCharacterTalk(string characterName)
    {
        if (currentCharacterTalk.isValid())
        {
            currentCharacterTalk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            currentCharacterTalk.release();
        }

        //print("current mf talking: " + characterName);
        switch(characterName)
        {
            case "Wysteria":
                currentCharacterTalk = RuntimeManager.CreateInstance(FMODEvents.Instance.vampireTalk);
                break;
            case "Dullahan":
                currentCharacterTalk = RuntimeManager.CreateInstance(FMODEvents.Instance.dullahanTalk);
                break;
            case "Aloe":
                currentCharacterTalk = RuntimeManager.CreateInstance(FMODEvents.Instance.aloeTalk);
                break;
            case "Vervain":
                // TODO: swap to vervainTalk when its added
                // currentCharacterTalk = RuntimeManager.CreateInstance(FMODEvents.Instance.aloeTalk);
                break;
        }

        currentCharacterTalk.start();
    }

    public void PlayPlayerHurt(string player)
    {
        if (!gathererHurt.isValid())
        {
            gathererHurt = CreateEventInstance(FMODEvents.Instance.gathererHurt);
        }
        if (!wardenHurt.isValid())
        {
            wardenHurt = CreateEventInstance(FMODEvents.Instance.wardenHurt);
        }
        if (player != null) 
        { 
            if (player.ToLower() == "gatherer")
            {
                PLAYBACK_STATE playbackState;
                gathererHurt.getPlaybackState(out playbackState);

                if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                {
                    gathererHurt.start();
                }
            } 
            else
            {
                PLAYBACK_STATE playbackState;
                wardenHurt.getPlaybackState(out playbackState);

                if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                {
                    wardenHurt.start();
                }
            }
        }
    }

    public void CleanUp()
    {
        Debug.Log("Sound EventInstance CleanUp");
        //Debug.Log(eventInstances.Count);
        foreach (EventInstance eventInstance in eventInstances)
        {
            //Debug.Log("hello");
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        eventInstances.Clear();

        currentCharacterTalk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        currentCharacterTalk.release();
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
