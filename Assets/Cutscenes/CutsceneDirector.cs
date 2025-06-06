using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneDirector : MonoBehaviour
{
    PlayableDirector currentDirector;
    bool firstPause;
    public GameObject HelperText;
    public bool endingCutscene;

    private void Start() {
        Time.timeScale = 1; //This line is needed as somehow sceneHandler doesnt change time back to 1 when from boss scene?
        currentDirector = gameObject.GetComponent<PlayableDirector>();
        firstPause = true;
        HelperText.SetActive(false);
    }

    public void WaitForInput() {
        if (currentDirector == null) {
            Debug.LogError("Using CutsceneDirector.cs without a timeline on the same object!");
        } else {
            currentDirector.Pause();
            if (firstPause) {
                HelperText.SetActive(true);
                firstPause = false;
            }
        }
    }

    public void AcceptInput() {
        if (currentDirector == null) {
            Debug.LogError("Using CutsceneDirector.cs without a timeline on the same object!");
        } else {
            currentDirector.Resume();
            HelperText.SetActive(false);
        }
    }

    public void EndCutscene() {
        //Debug.Log(endingCutscene + " ending cutscene ");
        if (endingCutscene)
        {
            //Debug.Log("Should transition to start menu!");
            SceneHandler.Instance.ToCreditsScene();
        }
        else
        {
            SceneHandler.Instance.ToHubScene();
        }
        
    }
}
