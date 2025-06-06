using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questID;
    
    protected void SetQuestString(string description)
    {
        QuestManager.Instance.questDisplay = $"<size=120%><color=#FEE17A>Quest:</color></size> {description}";
    }

    public void InitializeQuestStep(string questID)
    {
        this.questID = questID; 
    }
    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;
            EventManager.instance.questEvents.AdvanceQuest(questID);
            Destroy(this.gameObject);
        }

        
    }

    public void CancelQuestStep()
    {
        Destroy(this.gameObject);
    }
}
