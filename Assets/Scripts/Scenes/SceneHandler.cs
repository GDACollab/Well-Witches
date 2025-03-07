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
    private int LoadSceneIndex = 4;
    
    
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
        SceneManager.LoadScene(MainMenuSceneIndex);
    }
    public void ToLoadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int index = currentScene.buildIndex;
        // From Main Menu
        if (index == MainMenuSceneIndex)
        {

        }
        // From Hub
        else if (index == HubSceneIndex)
        {
            Debug.Log("Attempt to change scenes from " + currentScene.name + " to " + currentScene.name + " is not allowed");
            return;
        }
        // From Gameplay Scene
        else if (index == GameplaySceneIndex)
        {
            Debug.Log("Attempt to change scenes from " + currentScene.name + " to " + currentScene.name + " is not allowed");
            return;
        }
        // From Pause Scene
        else if (index == PauseSceneIndex)
        {
            Debug.Log("Attempt to change scenes from " + currentScene.name + " to " + currentScene.name + " is not allowed");
            return;
        }
        // Unsupported Scene
        else
        {
            Debug.Log("Transitions from the current scene, " + currentScene.name + " are not currently supported");
            return;
        }
        SceneManager.LoadScene(LoadSceneIndex);
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
        // From Load Scene
        else if(index == LoadSceneIndex)
        {

        }
        // Unsupported Scenes
        else{
            Debug.Log("Transitions from the current scene, " + currentScene.name + " are not currently supported");
            return;
        }
        SceneManager.LoadScene(HubSceneIndex);
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
        SceneManager.LoadScene(GameplaySceneIndex);
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
        SceneManager.LoadScene(PauseSceneIndex);
    }
}