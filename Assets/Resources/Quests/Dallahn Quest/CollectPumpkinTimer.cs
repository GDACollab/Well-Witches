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
    private StatsManager statsManager;
    private float gathererHealth = 0.0f;
    [SerializeField] public int timeToLive = 60;
    [SerializeField] public int hitsToLive = 3;
    
    //[SerializeField] public GameObject questItem;




    private void Start()
    {
        gathererHealth = statsManager.GathererCurrentHealth;
        //GameManager.instance.activeQuestPrefab = questItem;
        //GameManager.instance.activeQuestItemCount = garlicToCollect;
        questDescription.color = Color.white;
        questDescription.text = $"- Don't get hit (0/{hitsToLive}) times within the next (0/{timeToLive}) seconds";

    }

    void Update()
    {

        time += Time.deltaTime; // increment the timer
        int timeForDisplay = (int)(time % 60); // get the time for display
        questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next ({timeForDisplay}/{timeToLive}) seconds";
        float check = statsManager.GathererCurrentHealth;
        if (time >= timeToLive) // if timer hits without canceling the quest
        {

            questDescription.color = Color.green;
            questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next (60/60) seconds";
            FinishQuestStep();
            SceneHandler.Instance.ToHubScene();

        }

        if (hits >= hitsToLive) // if player gets hit
        {
            questDescription.color = Color.red;
            questDescription.text = $"You failed, now you must restart the quest";
            CancelQuestStep();
            SceneHandler.Instance.ToHubScene();
        }
        // Want to check to see if the player is first the gatherer, then detect if the player was hit by an enemy by seeing if it had taken damage. 
        if (gathererHealth > check)
        {

            gathererHealth = statsManager.GathererCurrentHealth;
            hits++;    
            questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next ({timeForDisplay}/{timeToLive}) seconds";

            
        }
            
            

            
             
        
        
        
        

    }

    
    
}  
