using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneDirector : MonoBehaviour
{
    PlayableDirector currentDirector;

    private void Start() {
        currentDirector = gameObject.GetComponent<PlayableDirector>();
    }

    public void WaitForInput() {
        if (currentDirector == null) {
            Debug.LogError("Using CutsceneDirector.cs without a timeline on the same object!");
        } else {
            currentDirector.Pause();
        }
    }

    public void AcceptInput() {
        if (currentDirector == null) {
            Debug.LogError("Using CutsceneDirector.cs without a timeline on the same object!");
        } else {
            currentDirector.Resume();
        }
    }

    public void EndCutscene() {
        SceneHandler.Instance.ToHubScene();
    }
}
