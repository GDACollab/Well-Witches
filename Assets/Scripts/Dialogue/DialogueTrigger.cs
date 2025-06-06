using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Queue")]
    [SerializeField] private GameObject visualCue;

    [Header("Reward Gameobject")]
    [SerializeField] private GameObject reward;

    [Header("Quest Manager")]
    [SerializeField] private QuestManager qm;

    [Header("JSON Dialogue File")]
    [SerializeField] private TextAsset JSON;
    private Story story;
    private QuestState questState;

    private InkDialogueVariables inkDialogueVariables;

    private InkExternalFunctions inkExternalFunctions;

    //bandage fix :(
    static private bool wisteriaDone = false;
    static private bool dullahanDone = false;
    static private bool phillipDone = false;

    [SerializeField] private QuestInfo quest;

    private bool playerInRange;

    private Controls controls => GathererAbilityManager.Controls;


    private void OnEnable()
    {
        EventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void Start()
    {
        controls.Gameplay_Gatherer.Interact.performed += OnGathererInteract; 
    }


    private void OnSceneChange(Scene before, Scene current)
    {   
        if(wisteriaDone && this.quest.id == "CollectGarlicQuest")
        {
            questState = QuestState.FINISHED;
            return;
        }
        else if(phillipDone && this.quest.id == "CollectFishQuest")
        {
            questState = QuestState.FINISHED;
            return;
        }
        else if (dullahanDone && this.quest.id == "CollectPumpkinQuest")
        {
            questState = QuestState.FINISHED;
            return;
        }
        if (current.name.Equals("Hub Scene"))
        {
            if (GameManager.instance.activeQuestID != null && GameManager.instance.activeQuestID == quest.id)
            {
                if (GameManager.instance.activeQuestState == QuestState.CAN_FINISH)
                {
                    questState = QuestState.CAN_FINISH;
                }
                else if (GameManager.instance.activeQuestState == QuestState.IN_PROGRESS)
                {
                    questState = QuestState.IN_PROGRESS;
                }
            }
            else if (GameManager.instance.activeQuestID == null)
            {
                Debug.Log("RAHH");
                questState = QuestState.CAN_START;
            }
        }
    }

    private void OnDisable()
    {
        EventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        SceneManager.activeSceneChanged -= OnSceneChange;
        controls.Gameplay_Gatherer.Interact.performed -= OnGathererInteract;
    }

    public void QuestStateChange(Quest quest)
    {
        if (this.quest != null)
        {
            if (this.quest.name == quest.info.name)
            {
                questState = quest.state;
                if (quest.state == QuestState.FINISHED)
                {
                    SpawnQuestReward();
                    if(this.quest.id == "CollectGarlicQuest")
                    {
                        wisteriaDone = true;
                    }
                    else if(this.quest.id == "CollectFishQuest")
                    {
                        phillipDone = true;
                    }
                    else if(this.quest.id == "CollectPumpkinQuest")
                    {
                        dullahanDone = true;
                    }
                }
            }
        }
    }

    private void SpawnQuestReward()
    {
        //TODO - might want to implement this better? this seems a bit too dependent
        foreach (Transform child in transform.parent.transform)
        {
            if (child.gameObject.name.Equals("rewardPoint"))
            {
                Instantiate(reward, child.transform);
                break;
            }
        }
    }
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        Debug.Log("I will be dying soon: " + GameManager.instance.activeQuestState);
        if (playerInRange && !DialogueManager.GetInstance().dialogueActive)
        {
            if (!visualCue.activeSelf) {
                AudioManager.Instance.PlayOneShot(FMODEvents.Instance.talkPrompt, this.transform.position);
            }
            visualCue.SetActive(true);
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnGathererInteract(InputAction.CallbackContext context)
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueActive && visualCue)
        {
            StatsManager.Instance.players["Warden"].GetComponent<PlayerMovement>().canMove = false;
            StatsManager.Instance.players["Gatherer"].GetComponent<PlayerMovement>().canMove = false;
            story = new Story(JSON.text);
            inkExternalFunctions = new InkExternalFunctions();
            inkDialogueVariables = new InkDialogueVariables(story);
            inkDialogueVariables.UpdateVariableState(quest.name + "State", new StringValue(questState.ToString()));
            inkExternalFunctions.Bind(story);
            DialogueManager.GetInstance().StartDialogueMode(story, GetComponentInParent<SpriteManager>(), inkDialogueVariables);
        }
    }

    private void OnDestroy()
    {
        if(inkExternalFunctions != null)
        {
            inkExternalFunctions.Unbind(story);
        }
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
