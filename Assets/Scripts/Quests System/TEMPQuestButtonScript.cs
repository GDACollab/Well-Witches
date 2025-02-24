using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEMPQuestButtonScript : MonoBehaviour
{
    private Button b;
    public void sendSignal()
    {
        EventManager.instance.questEvents.CancelQuest();
        b.interactable = false;
    }

    private void OnEnable()
    {
        EventManager.instance.questEvents.onStartQuest += StartQuest;
        EventManager.instance.questEvents.onFinishQuest += FinishQuest;
        EventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        EventManager.instance.questEvents.onStartQuest -= StartQuest;
        EventManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        b = gameObject.GetComponent<Button>();
        b.interactable = false;
    }

    public void StartQuest(string id)
    {
        b.interactable = true;
    }

    public void FinishQuest(string id)
    {
        b.interactable = false;
    }

    public void QuestStateChange(Quest q)
    {
        if(q.state == QuestState.CAN_FINISH)
        {
            b.interactable = false;
        }
    }

}
