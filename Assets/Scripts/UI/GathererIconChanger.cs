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
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[3].width, Icons[3].height) / 9f;
                break;
            case "SharingIsCaring":
                ActiveIcon.GetComponent<RawImage>().texture = Icons[4];
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[4].width, Icons[4].height) / 9f;
                break;
            case "BubbleBarrier":
                ActiveIcon.GetComponent<RawImage>().texture = Icons[5];
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[5].width, Icons[5].height) / 9f;
                break;
        }

        switch (Passive)
        {
            case "AloeVera":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[0];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[0].width, Icons[0].height) / 9f;
                break;
            case "HellfireBooties":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[1];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[1].width, Icons[1].height) / 9f;
                break;
            case "Espresso":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[2];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[2].width, Icons[2].height) / 9f;
                break;
            default:
                PassiveIcon.GetComponent<RawImage>().texture = Icons[6];
                break;
        }
    }
}
