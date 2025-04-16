using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GathererAbilityManager : MonoBehaviour
{
    public GathererBaseAbilities equipedAbility;
    public PassiveAbilities passiveAbility;
    [SerializeField] private Controls controls;

    public static GathererAbilityManager Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake()
    {
        InitSingleton();

        controls = new Controls();
        controls.Gameplay_Gatherer.Enable();
    }

    // Subscribe to the Gatherer controls input action asset "Activate Ability" action
    void OnEnable()
    {
        controls.Gameplay_Gatherer.ActivateAbility.performed += OnActivateAbility;
        controls.Gameplay_Gatherer.ActivateAbilityAfterHold.performed += OnActivateAbilityAfterHold;
    }
    void OnDisable()
    {
        controls.Gameplay_Gatherer.ActivateAbility.performed -= OnActivateAbility;
        controls.Gameplay_Gatherer.ActivateAbilityAfterHold.performed -= OnActivateAbilityAfterHold;
    }


    private void Start()
    {
        equipedAbility = AbilityHealthTransfer.Instance;
        passiveAbility = HealForcePassive.Instance;
        //print("equip");
    }
    void OnActivateAbility(InputAction.CallbackContext context)
    {
        print("using gatherer ability");
        if (equipedAbility != null)
        {
            if (equipedAbility.abilityName != "FlashStun")
            {
                equipedAbility.useAbility();
            }
        }
    }

    void OnActivateAbilityAfterHold(InputAction.CallbackContext context)
    {
        if (equipedAbility.abilityName == "FlashStun")
        {
            equipedAbility.useAbility();
        }
    }
    private void Update()
    {
        if (passiveAbility != null)
        {
            passiveAbility.passiveUpdate();
        }
    }

    /*
     TODO: Write a function that takes in an instance of an ability and sets "equipedAbility" to that instance
        will likley be called by whatever UI stuff selects abilities
     */
}