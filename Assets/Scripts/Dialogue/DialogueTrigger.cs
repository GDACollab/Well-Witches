using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Queue")]
    [SerializeField] private GameObject visualCue;

    [Header("JSON Dialogue File")]
    [SerializeField] private TextAsset JSON;
    private Story story;
    private QuestState questState;

    private InkDialogueVariables inkDialogueVariables;

    private InkExternalFunctions inkExternalFunctions;

    [SerializeField] private QuestInfo quest;

    private bool playerInRange;


    private void OnEnable()
    {
        EventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        EventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }
    public void QuestStateChange(Quest quest)
    {
        if(this.quest.name == quest.info.name)
        {
            questState = quest.state;
        }
    }
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueActive)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyUp(KeyCode.E)) //TODO: CHANGE THIS TO THE PLAYER INTERACT BUTTON 
            {
                story = new Story(JSON.text);
                inkExternalFunctions = new InkExternalFunctions();
                inkDialogueVariables = new InkDialogueVariables(story);
                inkDialogueVariables.UpdateVariableState(quest.name + "State", new StringValue(questState.ToString()));
                inkExternalFunctions.Bind(story);
                DialogueManager.GetInstance().StartDialogueMode(story, GetComponentInParent<SpriteManager>(), inkDialogueVariables);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        inkExternalFunctions.Unbind(story);
    }

    // Player has not been made yet, but when it is, make sure it is tagged with Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }

    //TESTING ONLY CODE BELOW, DO NOT UNCOMMENT :(
    //private void OnMouseEnter()
    //
    //    playerInRange = true;
    //}

    //private void OnMouseExit()
    //{
    //    playerInRange = false;
    //}
}
