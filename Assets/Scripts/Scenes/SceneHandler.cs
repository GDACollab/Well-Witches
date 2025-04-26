using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }
        
    [SerializeField]
    private int MainMenuSceneIndex = 0;
    [SerializeField]
    private int HubSceneIndex = 1;
    [SerializeField]
    private int GameplaySceneIndex = 2;
    [SerializeField]
    private int PauseSceneIndex = 3;
    [SerializeField]
    private int BossSceneIndex = 4;

    [Header("Transition Screen")]
    [Tooltip("Image for loading screen")]
    [SerializeField] private GameObject loadScreen;
    [Tooltip("Image for loading screen")]
    [SerializeField] private float waitTime = 2f;
    
    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnApplicationQuit(){
        Instance = null;
    }

    // FOR TESTING PURPOSES ONLY DO NOT UNCOMMENT
/*
    public void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            ToMainMenuScene();
        }
        if(Input.GetKeyDown(KeyCode.S)){
            ToHubScene();
        }
        if(Input.GetKeyDown(KeyCode.D)){
            ToGameplayScene();
        }
        if(Input.GetKeyDown(KeyCode.F)){
            ToPauseScene();
        }
    }
*/

    public void ToMainMenuScene(){
        Scene currentScene = SceneManager.GetActiveScene();
        int index = currentScene.buildIndex;
        // From Main Menu
        if(index == MainMenuSceneIndex){
            Debug.Log("Attempt to change scenes from " + currentScene.name + " to " + currentScene.name + " is not allowed");
            return;
        }
        // From Hub
        else if(index == HubSceneIndex){
            
        }
        // From Gameplay Scene
        else if(index == GameplaySceneIndex){

        }
        // From Pause Scene
        else if(index == PauseSceneIndex){
            
        }
        // Unsupported Scene
        else{
            Debug.Log("Transitions from the current scene, " + currentScene.name + " are not currently supported");
            return;
        }

        StartCoroutine(LoadingScreen(PauseSceneIndex));
        //SceneManager.LoadScene(MainMenuSceneIndex);
    }
    public void ToHubScene(){

        Scene currentScene = SceneManager.GetActiveScene();
        int index = currentScene.buildIndex;
        // From Main Menu
        if(index == MainMenuSceneIndex){
            
        }
        // From Hub
        else if(index == HubSceneIndex){
            Debug.Log("Attempt to change scenes from " + currentScene.name + " to " + currentScene.name + " is not allowed");
            return;
        }
        // From Gameplay Scene
        else if(index == GameplaySceneIndex){

        }
        // From Pause scene
        else if(index == PauseSceneIndex){
            
        }
        else if(index == BossSceneIndex){
            
        }
        // Unsupported Scenes
        else{
            Debug.Log("Transitions from the current scene, " + currentScene.name + " are not currently supported");
            return;
        }

        //SceneManager.LoadScene(HubSceneIndex);
        StartCoroutine(LoadingScreen(HubSceneIndex)); 

        AudioManager.Instance.CleanUp();
        AudioManager.Instance.PlayOST(FMODEvents.Instance.lobbyBGM);
    }
    public void ToGameplayScene(){
        Scene currentScene = SceneManager.GetActiveScene();
        int index = currentScene.buildIndex;
        // From Main Menu
        if(index == MainMenuSceneIndex){
            
        }
        // From Hub
        else if(index == HubSceneIndex){
            
        }
        // From Gameplay Scene
        else if(index == GameplaySceneIndex){
            Debug.Log("Attempt to change scenes from " + currentScene.name + " to " + currentScene.name + " is not allowed");
            return;
        }
        // From Pause Scene
        else if(index == PauseSceneIndex){
            
        }
        // Unsupported Scene
        else{
            Debug.Log("Transitions from the current scene, " + currentScene.name + " are not currently supported");
            return;
        }

        //SceneManager.LoadScene(GameplaySceneIndex);
        StartCoroutine(LoadingScreen(PauseSceneIndex));

        AudioManager.Instance.CleanUp();
        AudioManager.Instance.PlayOST(FMODEvents.Instance.mainMapBGM);
    }
    public void ToPauseScene(){
        Scene currentScene = SceneManager.GetActiveScene();
        int index = currentScene.buildIndex;
        // From Main Menu
        if(index == MainMenuSceneIndex){
            
        }
        // From Hub
        else if(index == HubSceneIndex){
            
        }
        // From Gameplay Scene
        else if(index == GameplaySceneIndex){
            
        }
        // From Pause Scene
        else if(index == PauseSceneIndex){
            Debug.Log("Attempt to change scenes from " + currentScene.name + " to " + currentScene.name + " is not allowed");
            return;
        }
        // Unsupported Scene
        else{
            Debug.Log("Transitions from the current scene, " + currentScene.name + " are not currently supported");
            return;
        }

        StartCoroutine(LoadingScreen(PauseSceneIndex));

        //SceneManager.LoadScene(PauseSceneIndex);
    }


    public void ToBossScene(){
        Scene currentScene = SceneManager.GetActiveScene();
        int index = currentScene.buildIndex;
        SceneManager.LoadScene(BossSceneIndex);

        AudioManager.Instance.CleanUp();
        AudioManager.Instance.PlayOST(FMODEvents.Instance.bossBGM);
    }

    //show image, wait x seconds, load scene
    private IEnumerator LoadingScreen(int sceneName)
    {
        //show picture, backup incase some scene doesn't have it
        if (loadScreen != null)
        {
            loadScreen.SetActive(true);
        }else
        {
            Debug.Log("ERROR: MISSING LOADING SCREEN");
        }

        //animation will be done via art

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadSceneAsync(sceneName);
    }

}