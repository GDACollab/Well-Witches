using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatCodeHandler : MonoBehaviour
{
    public InputActionAsset inputActions;
    public GameObject Abilities;
    public BossWell bossTrigger;

    private InputAction cheatCodeAction;


    private void OnEnable()
    {
        var cheatMap = inputActions.FindActionMap("Cheat Code");
        cheatCodeAction = cheatMap.FindAction("Key Combination");
        cheatCodeAction.Enable();
        cheatCodeAction.performed += OnCheatCodePressed;
    }

    private void OnDisable()
    {
        cheatCodeAction.Disable();
        cheatCodeAction.performed -= OnCheatCodePressed;
    }

    private void OnCheatCodePressed(InputAction.CallbackContext ctx)
    {
        Debug.Log("Cheat code activated! ALL ABILTIIES SHOULD BE UNLOCKED NOW");
        foreach (Transform child in Abilities.transform)
        {
            //Debug.Log("Child name: " + child.gameObject.GetComponent<AbilitySelectIndividualAbilities>().getAbilityName());
            child.gameObject.GetComponent<AbilitySelectIndividualAbilities>().setLocked(false);
        }

        bossTrigger.ActivateWell();
    }
}
