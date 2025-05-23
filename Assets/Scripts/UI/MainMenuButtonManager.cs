using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonManager : MonoBehaviour
{
    public GameObject journal;
    public GameObject otherButtons;
    public void OnMainMenuPlayButtonPress()
    {
        SceneHandler.Instance.ToOpenCutscene();
        Debug.Log("going to hub from start screen");
    }

    public void OnMainMenuQuitButtonPress()
    {
        Application.Quit(); // quit game build
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    // quit game in editor
        #endif
    }

    public void OnSettingButtonPress()
    {
        //print("GOD WHY IS IT NOT WORKING");
        //print(journal.activeSelf);
        if (!journal.activeSelf)
        {
            otherButtons.SetActive(false);
            journal.SetActive(true);
        }
        else
        {
            otherButtons.SetActive(true);
            journal.SetActive(false);
        }
        
    }

    public void onCredits()
    {
        print("LOL its not done yet");
    }
}
