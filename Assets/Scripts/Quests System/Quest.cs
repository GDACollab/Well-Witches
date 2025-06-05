using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest
{
    // all our static info
    public QuestInfo info;

    // state info
    public QuestState state;
    private int currentQuestStepIndex;

    public Quest(QuestInfo questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
    }

    public void Reset()
    {
        this.currentQuestStepIndex = 0;
    }
    public void AdvanceToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = getCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id);
        }
    }

    private GameObject getCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if(CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("Tried to get quest step prefab, but index was out of range indicating that current step doesn't exist. " +
                "Quest ID: " + info.id + ", stepIndex=" + currentQuestStepIndex);
        }
        return questStepPrefab;
    }

}
