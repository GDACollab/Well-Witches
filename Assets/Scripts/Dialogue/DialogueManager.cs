using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor.ShaderGraph;
// any other editor-only logic
#endif

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] public GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices; 
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    private static DialogueManager instance;
    private InkDialogueVariables currentVars;
    public Boolean dialogueActive { get; private set; } //variable is read-only to outside scripts

    private SpriteManager currentCharacter;
    private PlayerSpriteManager playerSpriteManager;
    private enum PlayerState {WARDEN, GATHERER};
    private PlayerState activePlayer = PlayerState.WARDEN;

    private const string SPRITE_TAG = "sprite";
    private const string SPEAKER_TAG = "speaker";

    private Controls controls;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Dialogue Manager detected... :(");
        }
        instance = this;

        controls = new Controls();

        controls.Gameplay_Gatherer.Enable();
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChange;
        controls.Gameplay_Gatherer.Interact.performed += OnGathererInteract;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
        controls.Gameplay_Gatherer.Interact.performed -= OnGathererInteract;
    }

    public void OnSceneChange(Scene before, Scene current)
    {
        init();
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        init();
    }

    private void init()
    {
        dialogueActive = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject c in choices)
        {
            choicesText[index] = c.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        playerSpriteManager = gameObject.GetComponent<PlayerSpriteManager>();
    }

    private void Update()
    {
        Debug.Log("here: "+ dialogueActive);
        //do nothing if there is no dialogue activly playing
        if (!dialogueActive)
        {
            return;
        }

        //manage going to the next line when player clicks to continue
        //TODO: CHANGE THIS TO THE PLAYER INTERACT KEY
        //BUG: Currently can just press E to completely skip the choice
        //if we change this to a system where you have to use arrow keys and enter/interact to do choices than we can fix this in favor of a system-
        //-where we have a choice already selected and the player can navigate up or down to select another one before pressing enter
        if(Input.GetKeyDown(KeyCode.Space)) {
            if (currentStory.currentChoices.Count == 0)
            {
                ContinueStory();
            }
        }
    }

    private void OnGathererInteract(InputAction.CallbackContext context)
    {
        /*if (currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }*/
    }

    public void StartDialogueMode(Story story, SpriteManager currChara, InkDialogueVariables inkDialogueVariables)
    {
        currentStory = story;
        inkDialogueVariables.SyncVariablesAndStartListening(currentStory);
        currentVars = inkDialogueVariables;
        dialogueActive = true;
        dialoguePanel.SetActive(true);  

        // reset speaker
        speakerText.text = "???"; 

        // logic handled by the SpriteManager script
        currentCharacter = currChara;
        currentCharacter.DisplaySprite();
        playerSpriteManager.DisplayPlayerSprite();

        if ((string)currentStory.variablesState["currentSpeaker"] != "Player") {
            AudioManager.Instance.PlayCharacterTalk((string)currentStory.variablesState["currentSpeaker"]);
        }
        else {
            foreach (string tag in currentStory.currentTags) {
                string[] splitTag = tag.Split(':');
                string tagValue = splitTag[1].Trim();

                if (tagValue == "warden") {
                    AudioManager.Instance.PlayCharacterTalk("Vervain");
                }
                else if (tagValue == "gatherer") {
                    AudioManager.Instance.PlayCharacterTalk("Aloe");
                }
            }
        }
        ContinueStory();
    }

    private IEnumerator EndDialogueMode()
    {
        yield return new WaitForSeconds(0.2f); //in place so that the player doesnt instantly talk to the NPC again on accident

        dialogueActive = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        currentCharacter.HideSprite();
        playerSpriteManager.HidePlayerSprite();
        currentVars.StopListening(currentStory);
    }

    private void ContinueStory()
    {
        //sets dialogue text and advances through ink 
        if (currentStory.canContinue)
        {
            string text = currentStory.Continue();
            while (IsLineBlank(text) && currentStory.canContinue)
            {
                text = currentStory.Continue();
            }
            if(IsLineBlank(text) && !currentStory.canContinue)
            {
                StartCoroutine(EndDialogueMode());
            }
            else
            {
                dialogueText.text = text;
                DisplayChoices();
                HandleTagsNPC((string)currentStory.variablesState["currentSpeaker"], currentStory.currentTags);
            }

            print("current character name: " + (string)currentStory.variablesState["currentSpeaker"]);

            if ((string)currentStory.variablesState["currentSpeaker"] != "Player") {
                AudioManager.Instance.PlayCharacterTalk((string)currentStory.variablesState["currentSpeaker"]);
            }
            else {
                foreach (string tag in currentStory.currentTags) {
                    string[] splitTag = tag.Split(':');
                    string tagValue = splitTag[1].Trim();

                    if (tagValue == "warden") {
                        AudioManager.Instance.PlayCharacterTalk("Vervain");
                    }
                    else if (tagValue == "gatherer") {
                        AudioManager.Instance.PlayCharacterTalk("Aloe");
                    }
                }
            }
        }
        else
        {
            StartCoroutine(EndDialogueMode());
        }
    }

     private void HandleTagsNPC(string speakerName, List<string> currentTags)
    {
        speakerText.text = speakerName;
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags) 
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) 
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            
            // handle the tag
            switch (tagKey) 
            {
                case SPEAKER_TAG:
                    if (speakerName == "Player")
                    {
                        if (tagValue == "warden")
                        {
                            activePlayer = PlayerState.WARDEN;
                            playerSpriteManager.SwitchToWarden();
                            speakerText.text = char.ToUpper(tagValue.First()) + tagValue.Substring(1);
                        }
                        else if (tagValue == "gatherer")
                        {
                            activePlayer = PlayerState.GATHERER;
                            playerSpriteManager.SwitchToGatherer();
                            speakerText.text = char.ToUpper(tagValue.First()) + tagValue.Substring(1);
                        }
                    }
                    break;
                case SPRITE_TAG:
                    if (speakerName == "Player")
                    {
                        if (activePlayer == PlayerState.WARDEN)
                        {
                            playerSpriteManager.ChangeWardenSprite(tagValue);
                        }
                        else if (activePlayer == PlayerState.GATHERER)
                        {
                            playerSpriteManager.ChangeGathererSprite(tagValue);
                        }
                    }
                    else{
                        currentCharacter.ChangeSprite(tagValue);
                    }
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        //check to make sure there is not more choices than there are buttons
        if(currentChoices.Count > choices.Length) //choices.length is the amount of choices we can support
        {
            Debug.LogError("More choices were supplied than the choice UI can support :(.\nNumber of choices provided: " + currentChoices.Count + "\nNumber of choices suppported: " + choices.Length);
        }

        int index = 0;
        //initialize choices and enable buttons
        foreach(Choice c in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = c.text;
            index++;
        }

        //there might be left over choice buttons that are not being used, this disables them
        for (int i = index; i < choices.Length; i++) 
        {
            choices[i].gameObject.SetActive(false);
        }
    }
   
    public void MakeChoice(int choiceIndex)
    {
        List<String> choiceTags = currentStory.currentChoices[choiceIndex].tags;
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }
}
