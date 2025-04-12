using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_audio_play : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.testAudio, this.transform.position);
    }
}
