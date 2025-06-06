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

    private void SetQuestString() => SetQuestString($"Collect <color=#76FEC0>[{pumpkinCollected}/{pumpkinToCollect}] Pumpkins</color> for <color=#53141F>Dullhan</color>");

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
        SetQuestString();
    }

    private void PumpkinCollected()
    {
        if (pumpkinCollected < pumpkinToCollect)
        {
            pumpkinCollected++;
            SetQuestString();
        }

        if (pumpkinCollected >= pumpkinToCollect)
        {
            SetQuestString("<color=green>Success!</color> Report back to <color=#53141F>Dullhan</color>");
            FinishQuestStep();
        }
    }
}
