using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField]
    private int OpenCutsceneIndex = 5;
    [SerializeField]
    private int LoadingScreenIndex = 6;

    [Header("Transition Screen")]
    [Tooltip("Image for loading screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image fadeUIImage;
    [Tooltip("Image for loading screen")]
    [SerializeField] private float waitTime = 2f;
    [SerializeField] float fadeInTime = 1f;
    [SerializeField] float fadeOutTime = 1f;
    
    
    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            fadeUIImage.gameObject.SetActive(false);
            loadingScreen.SetActive(false);
        }
    }

    private void OnApplicationQuit(){
        Instance = null;
    }

    private void OnEnable()
    {
        
    }
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     SceneManager.sceneLoaded += (_,_) => fadeUIImage.gameObject.SetActive(false);
    // }

    public IEnumerator FadeFromBlack(float fadeInTime)
    {
        Time.timeScale = 0f;
        fadeUIImage.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(0.1f);
        Color objectColor = fadeUIImage.color; //Gets Object Color and Modifies values
        objectColor.a = 1f;
        fadeUIImage.color = objectColor;
        float timer = fadeInTime;
        while (fadeUIImage.color.a > 0)
        {
            timer -= Time.unscaledDeltaTime;
            objectColor.a = Mathf.Lerp(-0.1f, 1, timer / fadeInTime);
            fadeUIImage.color = objectColor;
            yield return null;
        }
        fadeUIImage.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public IEnumerator FadeToBlack(float fadeOutTime)
    {
        Time.timeScale = 0f;
        Color objectColor = fadeUIImage.color; //Gets Object Color and Modifies values
        objectColor.a = 0;
        fadeUIImage.color = objectColor;
        fadeUIImage.gameObject.SetActive(true);
        float timer = fadeOutTime;
        while (fadeUIImage.color.a < 1)
        {
            timer -= Time.unscaledDeltaTime;
            objectColor.a = Mathf.Lerp(1.1f, 0, timer / fadeOutTime);
            fadeUIImage.color = objectColor;
            yield return null;
        }
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(0.1f);
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
        // From Open Cutscene Scene
        else if (index == OpenCutsceneIndex) {

        }
        // Unsupported Scene
        else {
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
        // From Open Cutscene Scene
        else if (index == OpenCutsceneIndex) {

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
        StartCoroutine(LoadingScreen(GameplaySceneIndex));

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

    public void ToOpenCutscene() {

        Scene currentScene = SceneManager.GetActiveScene();
        int index = currentScene.buildIndex;
  
        //SceneManager.LoadScene(HubSceneIndex);
        StartCoroutine(LoadingScreen(OpenCutsceneIndex));

        AudioManager.Instance.CleanUp();
        AudioManager.Instance.PlayOST(FMODEvents.Instance.lobbyBGM);
    }
    public void ToBossScene(){
        Scene currentScene = SceneManager.GetActiveScene();
        int index = currentScene.buildIndex;
        //SceneManager.LoadScene(BossSceneIndex);
        StartCoroutine(LoadingScreen(BossSceneIndex));

        AudioManager.Instance.CleanUp();
        AudioManager.Instance.PlayOST(FMODEvents.Instance.bossBGM);
    }

    //show image, wait x seconds, load scene
    private IEnumerator LoadingScreen(int sceneName)
    {
        //show picture, backup incase some scene doesn't have it
        
        yield return FadeToBlack(fadeInTime);
        SceneManager.LoadScene(LoadingScreenIndex);
        loadingScreen.SetActive(true);
        yield return FadeFromBlack(fadeOutTime);
        //animation will be done via art
        AsyncOperation newscene = SceneManager.LoadSceneAsync(sceneName);
        newscene.allowSceneActivation = false;

        yield return new WaitForSecondsRealtime(waitTime);
        
        while (newscene.progress < 0.9f)
        {
            yield return null;
        }
        yield return FadeToBlack(fadeInTime);
        loadingScreen.SetActive(false);
        newscene.allowSceneActivation = true;
        yield return FadeFromBlack(fadeOutTime);
    }

}