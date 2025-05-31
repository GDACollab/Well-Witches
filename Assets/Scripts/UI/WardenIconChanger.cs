using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WardenIconChanger : MonoBehaviour
{
    [SerializeField] private List<Texture2D> Icons;
    [SerializeField] private GameObject ActiveIcon;
    [SerializeField] private GameObject PassiveIcon;

    void Update()
    {
        string Active = WardenAbilityManager.Instance.GetEquippedActiveName();
        string Passive = WardenAbilityManager.Instance.GetEquippedPassiveName();

        switch (Active)
        {
            case "DevastationBeam":
                ActiveIcon.GetComponent<RawImage>().texture = Icons[3];
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[3].width, Icons[3].height) / 9f;
                break;
            case "GourdForge":
                ActiveIcon.GetComponent<RawImage>().texture = Icons[4];
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[4].width, Icons[3].height) / 9f;
                break;
            case "SpellBurst":
                ActiveIcon.GetComponent<RawImage>().texture = Icons[5];
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[5].width, Icons[3].height) / 9f;
                break;
        }

        switch (Passive)
        {
            case "ResurrectionRegalia":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[0];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[0].width, Icons[0].height) / 9f;
                break;
            case "SoulSiphon":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[1];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[1].width, Icons[1].height) / 9f;
                break;
            case "BoggyBullets":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[2];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Icons[2].width, Icons[2].height) / 9f;
                break;
            default:
                PassiveIcon.GetComponent<RawImage>().texture = Icons[6];
                break;
        }
    }
}
