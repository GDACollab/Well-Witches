using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GathererAbilityManager : MonoBehaviour
{
    public GathererBaseAbilities equipedAbility;
    [SerializeField] string equipedAbilityName;
    public PassiveAbilities passiveAbility;
    [SerializeField] string passiveAbilityName;
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
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    void OnDisable()
    {
        controls.Gameplay_Gatherer.ActivateAbility.performed -= OnActivateAbility;
        controls.Gameplay_Gatherer.ActivateAbilityAfterHold.performed -= OnActivateAbilityAfterHold;
        SceneManager.activeSceneChanged -= ChangedActiveScene;
    }


    private void Start()
    {
        equipedAbility = Gatherer_FlashStun.Instance;
        equipedAbilityName = equipedAbility.abilityName;
        passiveAbility = HealForcePassive.Instance;
        passiveAbilityName = passiveAbility.abilityName;
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

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (equipedAbilityName != null)
        {
            switch (equipedAbilityName)
            {
                case "FlashStun":
                    equipedAbility = Gatherer_FlashStun.Instance;
                    break;
                case "HealthTransfer":
                    equipedAbility = AbilityHealthTransfer.Instance;
                    break;
                case "BubbleShield":
                    equipedAbility = BubbleShield.Instance;
                    break;
                default:
                    print("failed to swap to: " + equipedAbilityName);
                    break;
            }
        }

        if (passiveAbilityName != null)
        {
            switch (passiveAbilityName)
            {
                case "HealForce":
                    passiveAbility = HealForcePassive.Instance;
                    break;
                default:
                    print("failed to swap to: " + passiveAbilityName);
                    break;
            }
        }

    }

    /*
     TODO: Write a function that takes in an instance of an ability and sets "equipedAbility" to that instance
        will likley be called by whatever UI stuff selects abilities
     */
}