using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public QuestState activeQuestState;

    private void OnEnable()
    {
        EventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        EventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    public void QuestStateChange(Quest quest)
    {
        if(quest.state == QuestState.CAN_FINISH && SceneManager.GetActiveScene().buildIndex == 2)
        {
            activeQuestState = QuestState.CAN_FINISH;
        }
    }
    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Found more than one GameManager in the scene. Please make sure there is only one");
        else instance = this;
    }
}
