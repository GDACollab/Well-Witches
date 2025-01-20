using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Queue")]
    [SerializeField] private GameObject visualCue;

    [Header("JSON Dialogue File")]
    [SerializeField] private TextAsset JSON;

    private bool playerInRange;

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
                DialogueManager.GetInstance().StartDialogueMode(JSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
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
    private void OnMouseEnter()
    {
        playerInRange = true;
    }

    private void OnMouseExit()
    {
        playerInRange = false;
    }
}
