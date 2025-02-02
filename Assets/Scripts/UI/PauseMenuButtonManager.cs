using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuButtonManager : MonoBehaviour
{
    //TODO: hide ToHubButton when pausing from hub; only show it when pausing from gameplay

    [SerializeField] public GameObject pausePanel;

    public void OnPauseMenuHubButtonPress()
    {
        SceneHandler.Instance.ToHubScene();
        Debug.Log("going to hub from pause screen");
        Time.timeScale = 1;
    }

    public void OnPauseMenuQuitButtonPress()
    {
        Application.Quit(); // quit game build
        UnityEditor.EditorApplication.isPlaying = false;    // quit game in editor
    }

    public void OnPauseMenuResumeButtonPress()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
