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
                ActiveIcon.GetComponent<RectTransform>().sizeDelta = Icons[3].Size() / 2;
                break;
        }
    }
}
