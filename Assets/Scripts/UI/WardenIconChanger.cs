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
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        string Active = WardenAbilityManager.Instance.GetEquippedActiveName();
        string Passive = WardenAbilityManager.Instance.GetEquippedPassiveName();

        switch (Active)
        {
            case "DevastationBeam":
                ActiveIcon.GetComponent<RawImage>().texture = Icons[3];
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[3].Size() / 3;
                break;
            case "GourdForge":
                ActiveIcon.GetComponent<RawImage>().texture = Icons[4];
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[4].Size() / 3;
                break;
            case "SpellBurst":
                ActiveIcon.GetComponent<RawImage>().texture = Icons[5];
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[5].Size() / 3;
                break;
        }

        switch (Passive)
        {
            case "ResurrectionRegalia":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[0];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[0].Size() / 3;
                break;
            case "SoulSiphon":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[1];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[1].Size() / 3;
                break;
            case "BoggyBullets":
                PassiveIcon.GetComponent<RawImage>().texture = Icons[2];
                PassiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[2].Size() / 3;
                break;
            default:
                PassiveIcon.GetComponent<RawImage>().texture = Icons[6];
                break;
        }
    }
}
