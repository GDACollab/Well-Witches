using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    private Bus MasterBus => AudioManager.Instance.MasterBus;
    private Bus MusicBus => AudioManager.Instance.MusicBus;
    private Bus SFXBus => AudioManager.Instance.SFXBus;
    
    // Start is called before the first frame update
    void Start()
    {
        MasterBus.getVolume(out float temp);
        masterSlider.value = temp;
        masterSlider.onValueChanged.AddListener((value) => MasterBus.setVolume(value));
        // UNCOMMENT EVERYTHING BELOW WHEN BUSES ARE CORRECT AND DELETE THIS LINE
        MusicBus.getVolume(out temp);
        musicSlider.value = temp;
        musicSlider.onValueChanged.AddListener((value) => MusicBus.setVolume(value));
        SFXBus.getVolume(out temp);
        sfxSlider.value = temp;
        sfxSlider.onValueChanged.AddListener((value) => SFXBus.setVolume(value));
    }
}
