using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private GameObject reqMet;
    [SerializeField] private GameObject inProgress;
    [SerializeField] private GameObject canFinish;

    public void SetState(QuestState newState, bool startpoint, bool endpoint)
    {
        {
            reqMet.SetActive(false);
            inProgress.SetActive(false);
            canFinish.SetActive(false);

            switch (newState)
            {
                case QuestState.CAN_START:
                    if (startpoint) { reqMet.SetActive(true); }
                    break;
                case QuestState.IN_PROGRESS:
                    if (endpoint) { inProgress.SetActive(true); }
                    break;
                case QuestState.CAN_FINISH:
                    if (endpoint) { canFinish.SetActive(true); }
                    break;
                default: break;
            }
        }
    }
}
