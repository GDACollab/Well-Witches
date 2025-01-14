using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public static Global Instance {get; private set;}
    
    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }
    // Expected Scene Numbers:
    // Start menu: 0, Hub: 1, Gameplay scene: 2, Pause menu: 3
    public void ChangeScene(int from, int to){
        if(from == to){
            Debug.Log("Attempted to change to same scene. Scene Number: " + from.ToString());
            return;
        }
        if(from == 0){
            if(to == 1){
                SceneManager.LoadScene(1);
            }
        }
    }
}
