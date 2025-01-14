using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance {get; private set;}
    
    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }
}
