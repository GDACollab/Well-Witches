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
    
   private void SetQuestString() => SetQuestString($"Avoid damage <color=#76FEC0>[{hits}/{hitsToLive}]</color> times within <color=#76FEC0>[{time}/{timeToLive}]</color> seconds. <color=#5D5D9F>-Phillip</color>");
   
    
    private void Start()
    {
        //GameManager.instance.activeQuestPrefab = questItem;
        //GameManager.instance.activeQuestItemCount = garlicToCollect;
        SetQuestString();
    }

    private void PumpkinTimerQuest()
    {
        time += Time.deltaTime; // increase the time
        // pumpkin timer
        if (time < timeToLive)
        {
            //check the time
            SetQuestString();
        }
        
        if (playerHealth); // if player is hit
        {
            hits++; // increase the hits
            SetQuestString();
        }
        
        if (hits >= hitsToLive) // if timer hits 0 wihtout fail
        {
            SetQuestString("<color=red>FAILED</color>");
            // find the cancel function
            return; // cancel the quest step
        }
        if (time >= timeToLive) // if timer hits 0 without fail
        {
            SetQuestString("<color=green>Success!</color> Report back to <color=#5D5D9F>Phillip</color>");
            FinishQuestStep();
        }
        
    }
}
