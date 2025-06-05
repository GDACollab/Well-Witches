using System;
using UnityEngine;

public class QuestEvents
{
    public event Action<string> onStartQuest;
    public void StartQuest(string id)
    {
        if(onStartQuest != null)
        {
            onStartQuest(id);
        }
    }


    public event Action<string> onAdvanceQuest;
    public void AdvanceQuest(string id)
    {
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(id);
        }
    }

    public event Action<string> onFinishQuest;
    public void FinishQuest(string id)
    {
        if (onFinishQuest != null)
        {
            onFinishQuest(id);
        }
    }

    public event Action<Quest> onQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChange != null)
        {
            onQuestStateChange(quest);
        }
    }

    public event Action onCancelQuest;
    public void CancelQuest()
    {
        if (onCancelQuest != null)
        {
            onCancelQuest();
        }
    }

    public event Action<GameObject,int> onLoadItemsOnDeath;
    public void LoadItemsOnDeath(GameObject questItem, int amount)
    {
        if (onLoadItemsOnDeath != null)
        {
            onLoadItemsOnDeath(questItem,amount);
        }
    }

    public event Action onParcellaFinishedDialogue;
    public void ParcellaFinishedDialogue()
    {
        if (onParcellaFinishedDialogue != null)
        {
            onParcellaFinishedDialogue();
        }
    }

    public event Action onPhillipFishReturn;
    public void PhillipFishReturn()
    {
        if (onPhillipFishReturn != null)
        {
            onPhillipFishReturn();
        }
    }
}
