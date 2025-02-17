using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGarlicQuestStep : QuestStep
{
    private int garlicCollected = 0;
    private int garlicToCollect = 5;

    private void OnEnable()
    {
        EventManager.instance.miscEvent.onGarlicCollected += GarlicCollected;
    }

    private void OnDisable()
    {
        EventManager.instance.miscEvent.onGarlicCollected -= GarlicCollected;
    }

    private void GarlicCollected()
    {
        if (garlicCollected < garlicToCollect)
        {
            garlicCollected++;
        }

        if (garlicCollected >= garlicToCollect)
        {
            FinishQuestStep();
        }
    }
}
