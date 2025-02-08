using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuButtonManager : MonoBehaviour
{
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
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    // quit game in editor
        #endif
    }

    public void OnPauseMenuResumeButtonPress()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
