using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Test_Audio")]
    [field: SerializeField] public EventReference testAudio { get; private set; }

    [field: Header("Player Footsteps")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Spectral Shot")]
    [field: SerializeField] public EventReference spectralShot { get; private set; }

    [field: Header("Interact Bush")]
    [field: SerializeField] public EventReference interactBush { get; private set; }

    private static FMODEvents _instance;
    public static FMODEvents Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        } 
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


}
