using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuButtonManager : MonoBehaviour
{
    //TODO: hide ToHubButton when pausing from hub; only show it when pausing from gameplay

    // public int prevSceneIndex;

    [SerializeField] public GameObject hubButton;
    [SerializeField] public GameObject pausePanel;

    // get the index of the scene you paused from when pause scene is instantiated
    // private void Start()
    // {
    //     prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //     Debug.Log("paused from scene " + prevSceneIndex);

    //     Time.timeScale = 0;

    //     // don't want to show BacktoHub if we paused from hub
    //     if (prevSceneIndex == 1) { 
    //         hubButton.SetActive(false);
    //     } else {
    //         hubButton.SetActive(true);
    //     }
    // }

    public void OnPauseMenuHubButtonPress()
    {
        SceneHandler.Instance.ToHubScene();
        Debug.Log("going to hub from pause screen");
    }

    public void OnPauseMenuQuitButtonPress()
    {
        Application.Quit(); // quit game build
        UnityEditor.EditorApplication.isPlaying = false;    // quit game in editor
    }

    public void OnPauseMenuResumeButtonPress()
    {
        // not 100% sure what the pause implementation we want will be,
        // but for now we'll just go with manipulating timeScale
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        // if (prevSceneIndex == 1)
        // {
        //     SceneHandler.Instance.ToHubScene();
        // }
        // else if (prevSceneIndex == 2)
        // {
        //     SceneHandler.Instance.ToGameplayScene();
        // }
        // else
        // {
        //     Debug.Log("Error: no scene to return to");
        // }
    }
}
