using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Test_Audio")]
    [field: SerializeField] public EventReference testAudio { get; private set; }

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
