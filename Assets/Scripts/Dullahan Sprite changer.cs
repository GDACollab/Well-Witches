using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DullahanSpritechanger : MonoBehaviour
{

    public SpriteRenderer dullahanObj;
    public Sprite spriteToChangeTo;
    [SerializeField] private bool isQuestActive;

    private void OnEnable()
    {
        EventManager.instance.questEvents.onQuestStateChange += spriteChange;
    }

    private void OnDisable()
    {
        EventManager.instance.questEvents.onQuestStateChange -= spriteChange;
    }

    public void Start()
    {
        if (isQuestActive)
        {
            if (dullahanObj != null && spriteToChangeTo != null)
            {
                dullahanObj.sprite = spriteToChangeTo;
            }

        }
    }
    private void spriteChange(Quest Q)
    {
        if (Q.info.id == "CollectPumpkinQuest")
        {
            isQuestActive = true;
        }
    }


}
