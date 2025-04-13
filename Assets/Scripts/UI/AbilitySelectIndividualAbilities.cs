using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilitySelectIndividualAbilities : MonoBehaviour, IPointerEnterHandler
{

    [SerializeField] private Image abilityImage;
    [SerializeField] private Button abilityButton;
    [SerializeField] private AbilitySelectManager abilitySelectManager;

    [Header("Images")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite selectSprite;

    [Header("Text")]
    [SerializeField] private string abilityText;
    [SerializeField] private string abilityName;

    [Header("Ability Info")]
    [SerializeField] private bool isWarden;
    [SerializeField] private bool isActive;

    private int id;

    private void Start()
    {
        abilityImage.sprite = normalSprite;

        SpriteState spriteState = new SpriteState();
        spriteState.highlightedSprite = hoverSprite;
        abilityButton.spriteState = spriteState;
        abilityButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        abilitySelectManager.clickedAbility(isWarden, isActive, id);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        abilitySelectManager.updateHoveredAbility(id);
    }

    public void setSelected(bool selected)
    {
        if (selected)
        {
            abilityImage.sprite = selectSprite;
        }
        else
        {
            abilityImage.sprite = normalSprite;
        }
    }

    public bool getIsWarden()
    {
        return isWarden;
    }

    public bool getIsActive()
    {
        return isActive;
    }

    public string getAbilityText()
    {
        return abilityText;
    }

    public string getAbilityName()
    {
        return abilityName;
    }

    //Run at the start of the game in abilityselectmanager
    public void setID(int newID)
    {
        id = newID;
    }
}
