using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ParcellaTrigger : MonoBehaviour
{

    private bool playerInRange = false;

    [Header("Visual Queue")]
    [SerializeField] private GameObject visualCue;

    [Header("Visual Queue")]
    [Range(0.01f,1f)]
    [SerializeField] private float itemSpawnDelay = 0.2f;

    [Header("Item Spawn Offset")]
    [SerializeField] private Vector2 ItemSpawnOffset = new Vector2(0,-2);

    private List<(GameObject, int)> ItemsToReturn = new List<(GameObject, int)>();

    private Controls controls => GathererAbilityManager.Controls;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SceneChange;
        EventManager.instance.questEvents.onLoadItemsOnDeath += LoadItems;
        EventManager.instance.questEvents.onParcellaFinishedDialogue += FinishedSpeaking;
    }

    private void LoadItems(GameObject questItem, int amount)
    {
        ItemsToReturn.Add((questItem,amount));
    }
    private void SceneChange(Scene before, Scene after)
    {
        if(after.buildIndex == 2)
        {
            ItemsToReturn.Clear();
        }
    }
    private void OnDisable()
    {
        controls.Gameplay_Gatherer.Interact.performed -= OnGathererInteract;
        EventManager.instance.questEvents.onParcellaFinishedDialogue -= FinishedSpeaking;
    }

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Start()
    {
        controls.Gameplay_Gatherer.Interact.performed += OnGathererInteract;
    }

    private void FinishedSpeaking()
    {
        StartCoroutine(ReturnItems());
    }


    IEnumerator ReturnItems()
    {
        foreach (var item in ItemsToReturn)
        {
            for(int i = 0; i < item.Item2; i++)
            {
                GameObject itemDrop = Instantiate(
                    item.Item1,
        new Vector3(transform.position.x + ItemSpawnOffset.x, transform.position.y + ItemSpawnOffset.y, 0f),
        Quaternion.identity
        );
                yield return new WaitForSeconds(itemSpawnDelay);
            }
        }
        ItemsToReturn.Clear();
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
            // TESTING CODE
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void OnGathererInteract(InputAction.CallbackContext context)
    {
        //if (playerInRange)
        //{
           // StartCoroutine(ReturnItems());
        //}
    }
}
