using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CollectPumpkinTimer : QuestStep
{
    private float time = 0f;
    private int hits = 0;

    private float iFramesTime = 0f;
    private float failedQuestTimer = 0f; 
    private StatsManager statsManager;
    private float gathererHealth = 0f;


    [SerializeField] private int timeToLive = 60;
    [SerializeField] private int hitsToLive = 4;

    //[SerializeField] public GameObject questItem;





    private void Start()
    {
        int hits = 0;
        float time = 0f;
        float iFramesTime = 0f; 
        float failedQuestTimer = 0f;
        statsManager = StatsManager.Instance;
        var gathererHealth = statsManager.GathererCurrentHealth;
        print(gathererHealth);
        questDescription.color = Color.white;
        questDescription.text = $"- Don't get hit (0/{hitsToLive}) times within the next (0/{timeToLive}) seconds";

    }

    void Update()
    {

        time += Time.deltaTime; // increment the timer
        iFramesTime += Time.deltaTime; // increment the timer
        
        questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next ({(int)(time % 60)}/{timeToLive}) seconds";
        questDescription.color = Color.white;
        
        
        PumpkinTimerChecker();
        
        

    }
    private void PumpkinTimerChecker()
    {
        float check = statsManager.GathererCurrentHealth;
        
        if ((time > timeToLive)) // if timer hits without canceling the quest
        {

            questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next (60/60) seconds";
            questDescription.color = Color.green;
            FinishQuestStep();
            SceneHandler.Instance.ToHubScene();


        }
        
        if (hits >= hitsToLive) // if player gets hit
        {
            failedQuestTimer += Time.deltaTime; // increment the timer
            questDescription.text = $"You failed, now you must restart the quest";
            questDescription.color = Color.red;
            if (failedQuestTimer > 5f)
            {
                SceneHandler.Instance.ToHubScene();
                EventManager.instance.questEvents.CancelQuest();
                EventManager.instance.questEvents.StartQuest("MultiStepGarlicQuest");

            }
           
        }
        // Want to check to see if the player is first the gatherer, then detect if the player was hit by an enemy by seeing if it had taken damage. 
        if (gathererHealth != check)
        {
            if ((int)(iFramesTime) > 2)
            {
                iFramesTime = 0f; // reset the timer
                gathererHealth = statsManager.GathererCurrentHealth;
                hits++;
                questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next ({(int)(time % 60)}/{timeToLive}) seconds";
            }
        }
        
       
    }

    
    
}  
