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

    [SerializeField] public AbilitySelectIndividualAbilities[] abilitiesList;

    [SerializeField] private TextMeshProUGUI abilityInfoText;
    [SerializeField] private TextMeshProUGUI abilityNameText;

    [SerializeField] private GameObject abilityUIDisabler;

    [SerializeField] private string defaultAbilityText;

    private WardenAbilityManager wardenAbilityManager;
    private GathererAbilityManager gathererAbilityManager;

    void OnDestroy()
    {
        EventManager.instance.miscEvent.onShowAbilityUI -= OnAbilityMenuOpen;
    }

    //Toggles ui on gatherer interact
    public void OnAbilityMenuOpen() => StartCoroutine(OnAbilityMenuToggle());
    public IEnumerator OnAbilityMenuToggle()
    {
        yield return new WaitForSeconds(0.21f); // Dialogue manager enables controls after 0.2 seconds so we wait slightly longer
        if (abilityUIDisabler.activeSelf)
        {
            GathererAbilityManager.Controls.Gameplay_Gatherer.Enable();
            WardenAbilityManager.Controls.Gameplay_Warden.Enable();
            WardenAbilityManager.Controls.Ui_Navigate.Enable();
        }
        else
        {
            GathererAbilityManager.Controls.Gameplay_Gatherer.Disable();
            WardenAbilityManager.Controls.Gameplay_Warden.Disable();
            WardenAbilityManager.Controls.Ui_Navigate.Disable();
        }
        abilityUIDisabler.SetActive(!abilityUIDisabler.activeSelf);
    }

    private void Start()
    {
        wardenAbilityManager = WardenAbilityManager.Instance;
        gathererAbilityManager = GathererAbilityManager.Instance;

        int j = 0;
        for (int i = 0; i < abilitiesList.Length; i++)
        {
            abilitiesList[i].setID(i);

            if (abilitiesList[i].getAbilityID() == gathererAbilityManager.GetEquippedPassiveName())
            {
                selectedPassiveAbilityGatherer = selectAbilityDoStuff(-1, false, false, i);
            }
            else if (abilitiesList[i].getAbilityID() == gathererAbilityManager.GetEquippedActiveName())
            {
                selectedActiveAbilityGatherer = selectAbilityDoStuff(-1, false, true, i);
            }
            else if (abilitiesList[i].getAbilityID() == wardenAbilityManager.GetEquippedPassiveName())
            {
                selectedPassiveAbilityWarden = selectAbilityDoStuff(-1, true, false, i);
            }
            else if (abilitiesList[i].getAbilityID() == wardenAbilityManager.GetEquippedActiveName())
            {
                selectedActiveAbilityWarden = selectAbilityDoStuff(-1, true, true, i);
            }
        }

        updateHoveredAbility(-1);

        abilityUIDisabler.SetActive(false);
        
        EventManager.instance.miscEvent.onShowAbilityUI += OnAbilityMenuOpen;
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
                wardenAbilityManager.EquipActive(abilitiesList[selectedActiveAbilityWarden].getAbilityID());
            }
            else
            {
                selected = selectedPassiveAbilityWarden;
                selectedPassiveAbilityWarden = selectAbilityDoStuff(selected, warden, active, id);
                wardenAbilityManager.EquipPassive(abilitiesList[selectedPassiveAbilityWarden].getAbilityID());
            }
        }
        else
        {
            if (active)
            {
                selected = selectedActiveAbilityGatherer;
                selectedActiveAbilityGatherer = selectAbilityDoStuff(selected, warden, active, id);
                gathererAbilityManager.EquipActive(abilitiesList[selectedActiveAbilityGatherer].getAbilityID());
            }
            else
            {
                selected = selectedPassiveAbilityGatherer;
                selectedPassiveAbilityGatherer = selectAbilityDoStuff(selected, warden, active, id);
                gathererAbilityManager.EquipPassive(abilitiesList[selectedPassiveAbilityGatherer].getAbilityID());
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
}
