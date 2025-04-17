using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class AbilitySelectManager : MonoBehaviour
{

    [SerializeField] private int selectedPassiveAbilityWarden;
    [SerializeField] private int selectedActiveAbilityWarden;

    [SerializeField] private int selectedPassiveAbilityGatherer;
    [SerializeField] private int selectedActiveAbilityGatherer;

    [SerializeField] private AbilitySelectIndividualAbilities[] abilitiesList;

    [SerializeField] private TextMeshProUGUI abilityInfoText;
    [SerializeField] private TextMeshProUGUI abilityNameText;

    [SerializeField] private GameObject abilityUIDisabler;
    [SerializeField] private Controls controls;

    [SerializeField] private string defaultAbilityText;

    private void Awake()
    {
        controls = new Controls();

        controls.Gameplay_Gatherer.Enable();
    }

    private void OnEnable()
    {
        controls.Gameplay_Gatherer.Interact.performed += OnGathererInteract;
    }
    private void OnDisable()
    {
        controls.Gameplay_Gatherer.Interact.performed -= OnGathererInteract;
    }

    //Toggles ui on gatherer interact
    private void OnGathererInteract(InputAction.CallbackContext context)
    {
        abilityUIDisabler.SetActive(!abilityUIDisabler.active);
    }

    private void Start()
    {
        for (int i = 0; i < abilitiesList.Length; i++)
        {
            abilitiesList[i].setID(i);
        }

        updateHoveredAbility(-1);
    }

    public void clickedAbility(bool warden, bool active, int id)
    {
        int selected = -1;
        if (warden)
        {
            if (active)
            {
                selected = selectedActiveAbilityWarden;
                selectedActiveAbilityWarden = selectAbilityDoStuff(selected, warden, active, id);
            }
            else
            {
                selected = selectedPassiveAbilityWarden;
                selectedPassiveAbilityWarden = selectAbilityDoStuff(selected, warden, active, id);
            }
        }
        else
        {
            if (active)
            {
                selected = selectedActiveAbilityGatherer;
                selectedActiveAbilityGatherer = selectAbilityDoStuff(selected, warden, active, id);
            }
            else
            {
                selected = selectedPassiveAbilityGatherer;
                selectedPassiveAbilityGatherer = selectAbilityDoStuff(selected, warden, active, id);
            }
        }
    }

    private int selectAbilityDoStuff(int selected, bool warden, bool active, int id) {
        for (int i = 0; i < abilitiesList.Length; i++)
        {
            if (abilitiesList[i].getIsWarden() == warden && abilitiesList[i].getIsActive() == active)
            {
                abilitiesList[i].setSelected(false);
            }
        }

        if (selected == id)
        {
            abilitiesList[id].setSelected(false);
            selected = -1;
        }
        else
        {
            abilitiesList[id].setSelected(true);
            selected = id;
        }

        return selected;
    }

    public void updateHoveredAbility(int id)
    {
        if (id == -1)
        {
            abilityInfoText.text = defaultAbilityText;
            abilityNameText.text = " ";
            return;
        }

        abilityInfoText.text = abilitiesList[id].getAbilityText();
        abilityNameText.text = abilitiesList[id].getAbilityName();
    }

    public int getSelectedWardenPassive()
    {
        return selectedPassiveAbilityWarden;
    }

    public int getSelectedWardenActive()
    {
        return selectedActiveAbilityWarden;
    }

    public int getSelectedGathererPassive()
    {
        return selectedPassiveAbilityGatherer;
    }

    public int getSelectedGathererActive()
    {
        return selectedActiveAbilityGatherer;
    }
}
