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
    [SerializeField] private string abilityID; //Should be the same as what is in the "abilityName" field of each ability

    [Header("Images")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite selectSprite;
    [SerializeField] private Sprite lockedSprite;

    [Header("Text")]
    [SerializeField] private string abilityText;
    [SerializeField] private string abilityName;

    [Header("Ability Info")]
    [SerializeField] private bool isWarden;
    [SerializeField] private bool isActive;
    [SerializeField] private bool isLocked;

    private int id;

    private void Start()
    {
        setLocked(isLocked);
        abilityButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        if (!isLocked)
        {
            abilitySelectManager.clickedAbility(isWarden, isActive, id);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isLocked)
        {
            abilitySelectManager.updateHoveredAbility(id);
        }
    }

    public void setSelected(bool selected)
    {
        if (isLocked)
        {
            return;
        }

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

    public string getAbilityID()
    {
        return abilityID;
    }

    public void setLocked(bool locked)
    {
        isLocked = locked;

        SpriteState spriteState = new SpriteState();
        if (isLocked)
        {
            abilityImage.sprite = lockedSprite;
            spriteState.highlightedSprite = lockedSprite; //disables hover effect when sprite is locked
        }
        else
        {
            abilityImage.sprite = normalSprite;
            spriteState.highlightedSprite = hoverSprite;
        }

        abilityButton.spriteState = spriteState;
    }

    //Run at the start of the game in abilityselectmanager
    public void setID(int newID)
    {
        id = newID;
    }
}
