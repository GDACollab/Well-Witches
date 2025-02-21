using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questID;
    // Refrence to the GameObject that contains the textmeshpro for the UI
    public TextMeshProUGUI questDescription;

    public void InitializeQuestStep(string questID, TextMeshProUGUI questDescription)
    {
        this.questDescription = questDescription;
        this.questID = questID; 
    }
    protected void FinishQuestStep()
    {
        if(!isFinished)
        {
            isFinished = true;
            EventManager.instance.questEvents.AdvanceQuest(questID);
            Destroy(this.gameObject);
        }

        
    }
}
