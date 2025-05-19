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
   
    [SerializeField] public int timeToLive = 60;
    [SerializeField] public int hitsToLive = 3;
    protected StatsManager statsManager;
    //[SerializeField] public GameObject questItem;




    private void Start()
    {
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
        if (time >= timeToLive) // if timer hits without canceling the quest
        {

            questDescription.color = Color.green;
            questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next (60/60) seconds";
            FinishQuestStep();
            SceneHandler.Instance.ToHubScene();

        }
        
        
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {


            hits += 1;
            questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next ({(int)(time % 60)}/{timeToLive}) seconds";

        }
        if (hits >= hitsToLive)
        {
            questDescription.color = Color.red;
            questDescription.text = $"- Don't get hit ({hits}/{hitsToLive}) times within the next (60/60) seconds";
            CancelQuestStep();
        }
    }
    
}  
