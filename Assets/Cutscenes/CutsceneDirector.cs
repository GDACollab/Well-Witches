using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneDirector : MonoBehaviour
{
    PlayableDirector currentDirector;
    bool firstPause;
    public GameObject HelperText;

    private void Start() {
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
        SceneHandler.Instance.ToHubScene();
    }
}
