using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectFishQuestStep : QuestStep
{
    [SerializeField] public bool fishFound = false;
    [SerializeField] public GameObject questItem;
    [SerializeField] public GameObject fishBowl;
    [SerializeField] private Vector3 bowlOffset = new Vector2(-1, 0);

    private Transform gatherer;

    // the player should be able to complete quests if they are in the hub, since parcella might give them items to complete the quest
    private bool inHub = false;

    private bool collectedThisRun = false;

    private void SetQuestString() => SetQuestString($"'Find' a pet <color=#76FEC0>Ghost Fish</color> for <color=#FF91A4>Phillip</color>");

    private void OnEnable()
    {
        EventManager.instance.questEvents.onFishCollected += FishCollected;
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnDisable()
    {
        EventManager.instance.questEvents.onFishCollected -= FishCollected;
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void Start()
    {
        GameManager.instance.activeQuestPrefab = null;
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
                if (fishFound)
                {
                    CompleteQuest();
                }
            }
            else
            {
                // subtract neccesary garlic
                if (collectedThisRun)
                {
                    fishFound = false;
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

            // need to spawn fish bowl
            GameObject bowl = Instantiate(
                    fishBowl,
        new Vector3(gatherer.position.x + bowlOffset.x, gatherer.position.y + bowlOffset.y, 0f),
        Quaternion.identity
        );
        }
    }

    private void FishCollected()
    {
        if (!fishFound)
        {
            fishFound = true;
            collectedThisRun = true;
            SetQuestString();
        }

        if (fishFound && inHub)
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        SetQuestString("<color=green>Success!</color> Report back to <color=#FF91A4>Phillip</color>");
        FinishQuestStep();
    }
}
