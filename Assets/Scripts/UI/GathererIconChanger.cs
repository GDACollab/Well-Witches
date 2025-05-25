using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GathererIconChanger : MonoBehaviour
{
    [SerializeField] private List<Texture2D> Icons;
    [SerializeField] private GameObject ActiveIcon;
    [SerializeField] private GameObject PassiveIcon;

    void Update()
    {
        string Active = GathererAbilityManager.Instance.GetEquippedActiveName();
        string Passive = GathererAbilityManager.Instance.GetEquippedPassiveName();

        switch (Active)
        {
            case "SolarFlare":
                ActiveIcon.GetComponent<RawImage>().texture = Icons[3];
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[3].Size() / 9;
                break;
            case "SharingIsCaring":
                ActiveIcon.GetComponent<RawImage>().texture = Icons[4];
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[4].Size() / 9;
                break;
            case "BubbleBarrier":
                ActiveIcon.GetComponent<RawImage>().texture = Icons[5];
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[5].Size() / 9;
                break;
        }

        switch (Passive)
        {
            case "AloeVera":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[0];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[0].Size() / 9;
                break;
            case "HellfireBooties":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[1];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[1].Size() / 9;
                break;
            case "Espresso":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[2];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[2].Size() / 9;
                break;
            default:
                PassiveIcon.GetComponent<RawImage>().texture = Icons[6];
                break;
        }
    }
}
