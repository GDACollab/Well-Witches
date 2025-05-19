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
    private PlayerHealth playerHealth;
    [SerializeField] public int timeToLive = 60;
    [SerializeField] public int hitsToLive = 3;
    //[SerializeField] public GameObject questItem;
    
   
   
    
    private void Start()
    {
        //GameManager.instance.activeQuestPrefab = questItem;
        //GameManager.instance.activeQuestItemCount = garlicToCollect;
        questDescription.color = Color.white;
        questDescription.text = $"- Don't get hit (0/{hitsToLive}) times within the next (0/{timeToLive}) seconds";
        PumpkinTimerQuest();
    }

    private void Update()
    {
        time += Time.deltaTime; // increment the timer
        questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next ({time}/{timeToLive}) seconds";
        if (time >= timeToLive) // if timer hits 0 without fail
        {

            questDescription.color = Color.green;
            questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next (60/60) seconds";
            FinishQuestStep();
            
        }
    }
    private void PumpkinTimerQuest()
    {
        
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component not found on Gatherer object.");
            return;
        }
       
        
       
        
        
        /*if (hits >= hitsToLive) // if timer hits 0 wihtout fail
        {
            questDescription.color = Color.red;
             questDescription.text = $"- You failed to save the Pumpkin, you must start over and find a new one.";
            // find the cancel function
            CollectPumpkinQuest(); // cancel the quest step
        }
        //*/
        
       
        
    }
}
