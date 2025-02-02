using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonManager : MonoBehaviour
{
    public void OnMainMenuPlayButtonPress()
    {
        SceneHandler.Instance.ToHubScene();
        Debug.Log("going to hub from start screen");
    }

    public void OnMainMenuQuitButtonPress()
    {
        Application.Quit(); // quit game build
        UnityEditor.EditorApplication.isPlaying = false;    // quit game in editor
    }
}
