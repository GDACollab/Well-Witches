using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfo questInfo;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool endPoint = true;

    private bool playerNear;

    private string questID;

    private QuestState currentQuestState;

    private QuestIcon questIcon;

    private void Awake()
    {
        questID = questInfo.id;
        questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void OnEnable()
    {
        EventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    private void OnDisable()
    {
        EventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        // update the quest state of this questPoint ONLY IF the quest id matches the incoming quest
        if(quest.info.id.Equals(questID))
        {
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState,startPoint,endPoint);
        }
    }

    private void SpawnQuestReward(GameObject reward)
    {
        //TODO - might want to implement this better? this seems a bit too dependent
        foreach(Transform child in gameObject.transform)
        {
            if (child.gameObject.name.Equals("rewardPoint"))
            {
                Object.Instantiate(reward,child.transform);
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

    private void Update()
    {
        // TODO - Change this to use new input system, via making the input bus in the event manager
        if (Input.GetKeyUp(KeyCode.E) == true)
        {
            if (playerNear)
            {
                if(currentQuestState.Equals(QuestState.CAN_START) && startPoint)
                {
                    EventManager.instance.questEvents.StartQuest(questID);
                }
                else if(currentQuestState.Equals(QuestState.CAN_FINISH) && endPoint)
                {
                    EventManager.instance.questEvents.FinishQuest(questID);
                    SpawnQuestReward(questInfo.reward);
                }
            }
        }
    }
}
