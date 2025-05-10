using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("SFX")]
    [field: SerializeField] public EventReference testAudio { get; private set; }
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference spectralShot { get; private set; }
    [field: SerializeField] public EventReference interactBush { get; private set; }
    [field: SerializeField] public EventReference flashStun { get; private set; }
    [field: SerializeField] public EventReference flashStunHit { get; private set; }
    [field: SerializeField] public EventReference abilityReady { get; private set; }
    [field: SerializeField] public EventReference vampireTalk { get; private set; }
    [field: SerializeField] public EventReference dullahanTalk { get; private set; }
    [field: SerializeField] public EventReference bubbleActivate { get; private set; }
    [field: SerializeField] public EventReference bubbleDeactivate { get; private set; }
    [field: SerializeField] public EventReference bubbleDeflect { get; private set; }
    [field: SerializeField] public EventReference flamingPumpkinYank { get; private set; }

    [field: Header("BGM")]
    [field: SerializeField] public EventReference lobbyBGM { get; private set; }
    [field: SerializeField] public EventReference mainMapBGM { get; private set; }
    [field: SerializeField] public EventReference bossBGM { get; private set; }

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
