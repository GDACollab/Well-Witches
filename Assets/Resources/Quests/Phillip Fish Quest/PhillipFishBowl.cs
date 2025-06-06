using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
//using static UnityEditor.Progress;

public class PhillipFishBowl : MonoBehaviour
{

    [Header("Visual Queue")]
    [SerializeField] private GameObject visualCue;

    [Header("Visual Queue")]
    [SerializeField] private GameObject FishObj;

    [Header("Item Spawn Offset")]
    [SerializeField] private Vector2 ItemSpawnOffset = new Vector2(0, 2);

    [Header("Refrences")]
    [SerializeField] private GameObject fullFishBowlSprite;
    [SerializeField] private GameObject emptyFishBowlSprite;


    private bool playerInRange;
    private bool hasFish;

    private Controls controls => GathererAbilityManager.Controls;



    private void Awake()
    {
        playerInRange = false;
        hasFish = true;
        visualCue.SetActive(false);
    }

    private void Start()
    {
        controls.Gameplay_Gatherer.Interact.performed += OnGathererInteract;
    }

    private void OnEnable()
    {
        EventManager.instance.questEvents.onPhillipFishReturn += FishReturn;
    }

    private void OnDisable()
    {
        controls.Gameplay_Gatherer.Interact.performed -= OnGathererInteract;
        EventManager.instance.questEvents.onPhillipFishReturn -= FishReturn;
    }

    private void FishReturn()
    {
        hasFish = true;
        fullFishBowlSprite.SetActive(true);
        emptyFishBowlSprite.SetActive(false);
    }

    private void OnGathererInteract(InputAction.CallbackContext context)
    {
        if (visualCue && hasFish && playerInRange)
        {
            DropFish();
        }
    }

    private void Update()
    {
        if (playerInRange && hasFish)
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

    private void DropFish()
    {
        if (hasFish)
        {
            hasFish = false;
            GameObject itemDrop = Instantiate(
                    FishObj,
        new Vector3(transform.position.x + ItemSpawnOffset.x, transform.position.y + ItemSpawnOffset.y, 0f),
        Quaternion.identity
        );
            itemDrop.GetComponent<PhillipFish>().fishBowlPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            fullFishBowlSprite.SetActive(false);
            emptyFishBowlSprite.SetActive(true);
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
