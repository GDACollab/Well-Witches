using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class NonQuestDialogueTrigger : MonoBehaviour
{
    [Header("Visual Queue")]
    [SerializeField] private GameObject visualCue;

    [Header("JSON Dialogue File")]
    [SerializeField] private TextAsset JSON;
    private Story story;

    private string storyVarName = "death_count";

    private int interactionCount = 0;

    private InkDialogueVariables inkDialogueVariables;

    private bool playerInRange;

    private bool hasInteracted = false;

    private Controls controls => GathererAbilityManager.Controls;

    private void Start()
    {
        controls.Gameplay_Gatherer.Interact.performed += OnGathererInteract;
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnDisable()
    {
        controls.Gameplay_Gatherer.Interact.performed -= OnGathererInteract;
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void OnSceneChange(Scene before, Scene after)
    {
        if(after.name.Equals("Hub Scene"))
        {
            hasInteracted = false;
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
            if (!visualCue.activeSelf)
            {
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
            inkDialogueVariables = new InkDialogueVariables(story);
            if(!hasInteracted)
            {
                interactionCount++;
                hasInteracted = true;
            }
            inkDialogueVariables.UpdateVariableState(storyVarName, new StringValue(interactionCount.ToString()));
            DialogueManager.GetInstance().StartDialogueMode(story, GetComponentInParent<SpriteManager>(), inkDialogueVariables);
        }
    }

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
}
