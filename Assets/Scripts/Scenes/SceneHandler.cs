using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }

    public bool GenerationEnded = false;

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
    [SerializeField]
    private int EndingCutsceneIndex = 6;

    [Header("Transition Screen")]
    [Tooltip("Image for loading screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image fadeUIImage;
    [Tooltip("Image for loading screen")]
    [SerializeField] private float waitTime = 2f;
    [SerializeField] float fadeInTime = 1f;
    [SerializeField] float fadeOutTime = 1f;
    [Header("Helper Text")]
    [SerializeField] private LoadingScreenTextSO loadingScreenTextSO;
    [SerializeField] private TextMeshProUGUI helpText;
    
    
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
    
    public int GetHubSceneIndex() => HubSceneIndex;
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     SceneManager.sceneLoaded += (_,_) => fadeUIImage.gameObject.SetActive(false);
    // }

    public IEnumerator FadeFromBlack(float fadeInTime)
    {
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
    }

    public IEnumerator FadeToBlack(float fadeOutTime)
    {
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

        StartCoroutine(LoadingScreen(MainMenuSceneIndex));
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
        StartCoroutine(GameplayLoadingScreen(GameplaySceneIndex));

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
        StartCoroutine(CutsceneLoadingScreen(OpenCutsceneIndex));

        AudioManager.Instance.CleanUp();
        AudioManager.Instance.PlayOST(FMODEvents.Instance.lobbyBGM);
    }

    public void ToEndingCutscene()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        int index = currentScene.buildIndex;

        //SceneManager.LoadScene(HubSceneIndex);
        StartCoroutine(CutsceneLoadingScreen(EndingCutsceneIndex));

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
        Time.timeScale = 0;
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
        Time.timeScale = 1;
    }
    
    private IEnumerator CutsceneLoadingScreen(int sceneName)
    {
        Time.timeScale = 0;
        yield return FadeToBlack(fadeInTime);
        SceneManager.LoadScene(LoadingScreenIndex);
        AsyncOperation newscene = SceneManager.LoadSceneAsync(sceneName);
        newscene.allowSceneActivation = false;

        while (newscene.progress < 0.9f)
        {
            yield return null;
        }
        newscene.allowSceneActivation = true;
        yield return FadeFromBlack(fadeOutTime);
        Time.timeScale = 1;
    }
    
    //show image, wait x seconds, load scene
    private IEnumerator GameplayLoadingScreen(int sceneName)
    {
        Time.timeScale = 0;
        yield return FadeToBlack(fadeInTime);
        loadingScreen.SetActive(true);
        int randomTextNum = UnityEngine.Random.Range(0, loadingScreenTextSO.loadingTexts.Length);
        helpText.text = loadingScreenTextSO.loadingTexts[randomTextNum];
        yield return FadeFromBlack(fadeOutTime);
        SceneManager.LoadScene(sceneName);
        WardenAbilityManager.Controls.Ui_Navigate.Disable();

        while (!GenerationEnded)
        {
            yield return null;
        }
        
        yield return FadeToBlack(fadeInTime);
        loadingScreen.SetActive(false);
        yield return FadeFromBlack(fadeOutTime);
        WardenAbilityManager.Controls.Ui_Navigate.Enable();
        Time.timeScale = 1;
        GenerationEnded = false;
    }
}