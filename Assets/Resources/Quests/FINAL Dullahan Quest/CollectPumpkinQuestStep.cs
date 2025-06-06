using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectPumpkinQuestStep : QuestStep
{
    [SerializeField] public bool pumpkinFound = false;
    [SerializeField] public GameObject questItem;

    private Transform gatherer;

    // the player should be able to complete quests if they are in the hub, since parcella might give them items to complete the quest
    private bool inHub = false;

    private bool collectedThisRun = false;

    private void SetQuestString() => SetQuestString($"Find a new <color=#EC8541>Pumpkin Head</color> for <color=#52141F>Dullahan</color>");

    private void OnEnable()
    {
        EventManager.instance.questEvents.onPumpkinCollected += PumpkinCollected;
        SceneManager.activeSceneChanged += OnSceneChange;
        EventManager.instance.questEvents.onPumpkinFail += PumpkinFail;
    }

    private void OnDisable()
    {
        EventManager.instance.questEvents.onPumpkinCollected -= PumpkinCollected;
        SceneManager.activeSceneChanged -= OnSceneChange;
        EventManager.instance.questEvents.onPumpkinFail -= PumpkinFail;
    }

    private void PumpkinFail()
    {
        GameManager.instance.activeQuestPrefab = questItem;
    }

    private void Start()
    {
        GameManager.instance.activeQuestPrefab = questItem;
        GameManager.instance.activeQuestItemCount = 1;
        SetQuestString();
        gatherer = StatsManager.Instance.players["Gatherer"].transform;
    }


    private void OnSceneChange(Scene before, Scene after)
    {
        if (after.buildIndex == 1) //this is HUB's index
        {
            gatherer = StatsManager.Instance.players["Gatherer"].transform;
            inHub = true;
            if (GameManager.instance.diedOnLastRun == false)
            {
                // IF the player has not died, then update all the quest stuff the moment they return to hub
                // this should just be your code for checking whether the quest items were all done collecting/just move the actual finish code/check here
                if (pumpkinFound)
                {
                    CompleteQuest();
                }
            }
            else
            {
                // subtract neccesary garlic
                if (collectedThisRun)
                {
                    pumpkinFound = false;
                }
                SetQuestString();

                // tell parcella how many items to return
                EventManager.instance.questEvents.LoadItemsOnDeath(questItem, 1);
            }
        }
        else
        {
            inHub = false;
        }

        if (after.buildIndex == 2) // FOREST
        {
            gatherer = StatsManager.Instance.players["Gatherer"].transform;
            // if the player goes into the forest set collected this run to 0
            collectedThisRun = false;
        }
    }

    private void PumpkinCollected()
    {
        if (!pumpkinFound)
        {
            pumpkinFound = true;
            collectedThisRun = true;
            SetQuestString();
        }

        if (pumpkinFound && inHub)
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        SetQuestString("<color=green>Success!</color> Report back to <color=#52141F>Dullahan</color>");
        FinishQuestStep();
    }
}
