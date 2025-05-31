using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeSceneTrigger : MonoBehaviour
{
    public enum SceneSelection
    {
        GameplayScene,
        BossScene
    }

    [Header("Scene Selection")]
    [SerializeField] private SceneSelection sceneSelection;
    
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    private bool playerInTrigger;


    [SerializeField] private BossWell bossWell;

    private Controls controls => GathererAbilityManager.Controls;
    
    private void Awake()
    {
        playerInTrigger = false;
        visualCue.SetActive(false);
    }

    private void OnEnable()
    {
        controls.Gameplay_Gatherer.Interact.performed += OnGathererInteract;
    }

    private void OnDisable()
    {
        controls.Gameplay_Gatherer.Interact.performed -= OnGathererInteract;
    }

    private void Update()
    {
        if (playerInTrigger)
        {
            visualCue.SetActive(true);
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnGathererInteract(InputAction.CallbackContext context)
    {
        if (playerInTrigger)
        {
            if (sceneSelection == SceneSelection.GameplayScene)
            {
                SceneHandler.Instance.ToGameplayScene();
            }
            else if (sceneSelection == SceneSelection.BossScene && bossWell.canEnter)
            {
                SceneHandler.Instance.ToBossScene();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInTrigger = false;
    }

    // FOR TESTING AS THE PLAYER DOES NOT MOVE YET
    // private void OnMouseEnter() {
    //     playerInTrigger = true;
    // }
    // private void OnMouseExit() {
    //     playerInTrigger = false;
    // }
}
