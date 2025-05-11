using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CollectPumpkinQuest : QuestStep
{
    private int pumpkinCollected = 0;
    [SerializeField] public int pumpkinToCollect = 5;
    [SerializeField] public GameObject questItem;


    private void OnEnable()
    {
        EventManager.instance.miscEvent.onPumpkinCollected += PumpkinCollected;
    }

    private void OnDisable()
    {
        EventManager.instance.miscEvent.onPumpkinCollected -= PumpkinCollected;
    }

    private void Start()
    {
        GameManager.instance.activeQuestPrefab = questItem;
        GameManager.instance.activeQuestItemCount = pumpkinToCollect;
        questDescription.color = Color.white;
        questDescription.text = $"- (0/{pumpkinToCollect}) Pumpkin Collected";
    }

    private void PumpkinCollected()
    {
        if (pumpkinCollected < pumpkinToCollect)
        {
            pumpkinCollected++;
            questDescription.text = $"- ({pumpkinCollected}/{pumpkinToCollect}) Pumpkin Collected";
        }

        if (pumpkinCollected >= pumpkinToCollect)
        {
            questDescription.color = Color.green;
            FinishQuestStep();
        }
    }
}
