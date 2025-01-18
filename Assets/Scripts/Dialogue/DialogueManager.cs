using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using System;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;

    private static DialogueManager instance;

    public Boolean dialogueActive { get; private set; } //variable is read-only to outside scripts

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Dialogue Manager detected... :(");
        }
        instance = this;
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueActive = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        //do nothing if there is no dialogue activly playing
        if (!dialogueActive)
        {
            return;
        }

        //manage going to the next line when player clicks to continue
        //TODO: CHANGE THIS TO THE PLAYER INTERACT KEY
        if(Input.GetKeyUp(KeyCode.E)) {
            ContinueStory();
        }
    }

    public void StartDialogueMode(TextAsset JSON)
    {
        currentStory = new Story(JSON.text);
        dialogueActive = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void EndDialogueMode()
    {
        dialogueActive = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            EndDialogueMode();
        }
    }

}
