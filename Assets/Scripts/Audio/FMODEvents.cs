using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("SFX")]
    [field: SerializeField] public EventReference testAudio { get; private set; }
    [field: SerializeField] public EventReference wellArrive { get; private set; }
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
    [field: SerializeField] public EventReference lazerFire { get; private set; }
    [field: SerializeField] public EventReference gathererDown { get; private set; }
    [field: SerializeField] public EventReference gathererHurt{ get; private set; }
    [field: SerializeField] public EventReference wardenDown { get; private set; }
    [field: SerializeField] public EventReference wardenHurt{ get; private set; }
    [field: SerializeField] public EventReference tankAttackBash { get; private set; }
    [field: SerializeField] public EventReference tankTraverse { get; private set; }
    [field: SerializeField] public EventReference bruiserAttackSwipe  { get; private set; }
    [field: SerializeField] public EventReference bruiserAttackDash  { get; private set; }
    [field: SerializeField] public EventReference rangedAttackFire  { get; private set; }
    [field: SerializeField] public EventReference rangedTraversal  { get; private set; }
    [field: SerializeField] public EventReference rangedAttackHit  { get; private set; }
    [field: SerializeField] public EventReference bossLunge { get; private set; }
    [field: SerializeField] public EventReference bossPhase1Attack1  { get; private set; }
    [field: SerializeField] public EventReference bossPhase1Attack2  { get; private set; }
    [field: SerializeField] public EventReference itemPickUp  { get; private set; }
    [field: SerializeField] public EventReference aloeTalk  { get; private set; }
    [field: SerializeField] public EventReference vervainTalk  { get; private set; }
    [field: SerializeField] public EventReference diverTalk  { get; private set; }
    [field: SerializeField] public EventReference ghostMailTalk  { get; private set; }
    [field: SerializeField] public EventReference bossTalk  { get; private set; }
    [field: SerializeField] public EventReference hexTalk  { get; private set; }
    [field: SerializeField] public EventReference talkPrompt  { get; private set; }
    [field: SerializeField] public EventReference healthTransfer  { get; private set; }

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
