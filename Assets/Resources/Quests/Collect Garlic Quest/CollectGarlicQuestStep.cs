using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectGarlicQuestStep : QuestStep
{
    private int garlicCollected = 0;
    [SerializeField]public int garlicToCollect = 5;
    

    private void OnEnable()
    {
        EventManager.instance.miscEvent.onGarlicCollected += GarlicCollected;
    }

    private void OnDisable()
    {
        EventManager.instance.miscEvent.onGarlicCollected -= GarlicCollected;
    }

    private void Start()
    {
        questDescription.color = Color.white;
        questDescription.text = $"- (0/{garlicToCollect}) Garlic Collected";
    }

    private void GarlicCollected()
    {
        if (garlicCollected < garlicToCollect)
        {
            garlicCollected++;
            questDescription.text = $"- ({garlicCollected}/{garlicToCollect}) Garlic Collected";
        }

        if (garlicCollected >= garlicToCollect)
        {
            questDescription.color = Color.green;
            FinishQuestStep();
        }
    }
}
