using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] public GameObject pauseScene;
    [SerializeField] public GameObject hubButton;

    private void Awake()
    {
        pauseScene.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // not 100% sure what the pause implementation we want will be,
            // but for now we'll just go with manipulating timeScale

            // make sure all things you want to pause correctly use deltaTime...!
            if (SceneManager.GetActiveScene().buildIndex == 1) {
                hubButton.SetActive(false);
            } else {
                hubButton.SetActive(true);
            }
            
            pauseScene.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
