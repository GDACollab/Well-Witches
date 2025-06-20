using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectGarlicQuestStep : QuestStep
{
    private int garlicCollected = 0;
    [SerializeField]public int garlicToCollect = 5;
    [SerializeField] public GameObject questItem;

    // the player should be able to complete quests if they are in the hub, since parcella might give them items to complete the quest
    private bool inHub = false;

    private int collectedThisRun = 0;
    
    private void SetQuestString() => SetQuestString($"Collect <color=#76FEC0>[{garlicCollected}/{garlicToCollect}] Garlic</color> for <color=#B894D3>Wisteria</color>");

    private void OnEnable()
    {
        EventManager.instance.miscEvent.onGarlicCollected += GarlicCollected;
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnDisable()
    {
        EventManager.instance.miscEvent.onGarlicCollected -= GarlicCollected;
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void Start()
    {
        GameManager.instance.activeQuestPrefab = questItem;
        GameManager.instance.activeQuestItemCount = garlicToCollect;
        SetQuestString();
    }


    private void OnSceneChange(Scene before, Scene after)
    {
        if(after.buildIndex == 1) //this is HUB's index
        {
            inHub = true;
            if(GameManager.instance.diedOnLastRun == false)
            {
                // IF the player has not died, then update all the quest stuff the moment they return to hub
                // this should just be your code for checking whether the quest items were all done collecting/just move the actual finish code/check here
                if (garlicCollected >= garlicToCollect)
                {
                    CompleteQuest();
                }
            }
            else
            {
                // subtract neccesary garlic
                garlicCollected -= collectedThisRun;
                SetQuestString();

                // tell parcella how many items to return
                EventManager.instance.questEvents.LoadItemsOnDeath(questItem,collectedThisRun);
            }
        }
        else
        {
            inHub = false;
        }

        if(after.buildIndex == 2) // FOREST
        {
            // if the player goes into the forest set collected this run to 0
            collectedThisRun = 0;
        }
    }

    private void GarlicCollected()
    {
        if (garlicCollected < garlicToCollect)
        {
            garlicCollected++;
            collectedThisRun++;
            SetQuestString();
        }

        if (garlicCollected >= garlicToCollect && inHub)
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        SetQuestString("<color=green>Success!</color> Report back to <color=#B894D3>Wisteria</color>");
        FinishQuestStep();
    }
}
