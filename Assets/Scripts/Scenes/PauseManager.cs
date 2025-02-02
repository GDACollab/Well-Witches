using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] public GameObject pauseScene;

    private void Awake()
    {
        pauseScene.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScene.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
