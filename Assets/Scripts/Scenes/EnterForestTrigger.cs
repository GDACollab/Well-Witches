using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterForestTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] public GameObject visualCue;

    private bool playerInTrigger;
    
    private void Start()
    {
        playerInTrigger = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInTrigger)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyUp(KeyCode.E))  //TODO: CHANGE THIS TO THE PLAYER INTERACT BUTTON
            {
                SceneHandler.Instance.ToGameplayScene();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    // TODO: make sure player gets tagged with "Player" tag!!

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInTrigger = false;
        }
    }

    // FOR TESTING AS THE PLAYER DOES NOT MOVE YET
    private void OnMouseEnter() {
        playerInTrigger = true;
    }
    private void OnMouseExit() {
        playerInTrigger = false;
    }
}
