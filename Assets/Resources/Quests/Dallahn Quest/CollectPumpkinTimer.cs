using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CollectPumpkinTimer : QuestStep
{
    private int time = 0;
    private int hits = 0;
    [SerializeField] public int timeToLive = 60;
    [SerializeField] public int hitsToLive = 3;
    //[SerializeField] public GameObject questItem;


    private void OnEnable()
    {
        //EventManager.instance.miscEvent.onGarlicCollected += GarlicCollected;
    }

    private void OnDisable()
    {
        //EventManager.instance.miscEvent.onGarlicCollected -= GarlicCollected;
    }

    private void Start()
    {
        //GameManager.instance.activeQuestPrefab = questItem;
        //GameManager.instance.activeQuestItemCount = garlicToCollect;
        questDescription.color = Color.white;
        questDescription.text = $"- Don't get hit (0/{hitsToLive}) times within the next (0/{timeToLive}) seconds";
    }

    private void PumpkinTimerQuest()
    {
        // pumpkin timer
        if (time < timeToLive)
        {
            //check the time
            questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next ({time}/{timeToLive}) seconds";
        }
        if (hits >= hitsToLive) // if timer hits 0 wihtout fail
        {
            questDescription.color = Color.red;
            // find the cancel function
        }
        if (time >= timeToLive) // if timer hits 0 without fail
        {
            questDescription.color = Color.green;
            FinishQuestStep();
        }
    }
}
