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

    private int collectedThisRun = 0;
    

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
        questDescription.color = Color.white;
        questDescription.text = $"- (0/{garlicToCollect}) Garlic Collected";
    }


    private void OnSceneChange(Scene before, Scene after)
    {
        if(after.buildIndex == 1) //this is HUB's index
        {
            if(GameManager.instance.diedOnLastRun == false)
            {
                // IF the player has not died, then update all the quest stuff the moment they return to hub
                // this should just be your code for checking whether the quest items were all done collecting/just move the actual finish code/check here
                if (garlicCollected >= garlicToCollect)
                {
                    questDescription.color = Color.green;
                    FinishQuestStep();
                }
            }
            else
            {
                // subtract neccesary garlic
                garlicCollected -= collectedThisRun;
                questDescription.text = $"- ({garlicCollected}/{garlicToCollect}) Garlic Collected";

                // tell parcella how many items to return

            }
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
            questDescription.text = $"- ({garlicCollected}/{garlicToCollect}) Garlic Collected";
        }

        //if (garlicCollected >= garlicToCollect)
        //{
        //    questDescription.color = Color.green;
        //    FinishQuestStep();
        //}
    }
}
