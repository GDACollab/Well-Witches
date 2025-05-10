using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GathererAbilityManager : MonoBehaviour
{
    public GathererBaseAbilities equipedAbility;
    [SerializeField] public string equipedAbilityName;
    public PassiveAbilities passiveAbility;
    [SerializeField] public string passiveAbilityName;
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
        //EquipPassive("ZoneMomentum");
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
                    equipedAbility = GathererHealthTransfer.Instance;
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
                case "SolesOfTheDamned":
                    passiveAbility = SolesOfTheDamned.Instance;
                    break;
                case "ZoneMomentum":
                    passiveAbility = ZoneMomentum.Instance;
                    break;
                default:
                    print("failed to swap to: " + passiveAbilityName);
                    break;
            }
        }

    }
    public void EquipActive(string abilityID)
    {
        if (abilityID != null)
        {
            switch (abilityID)
            {
                case "FlashStun":
                    equipedAbility = Gatherer_FlashStun.Instance;
                    equipedAbilityName = abilityID;
                    break;
                case "HealthTransfer":
                    equipedAbility = GathererHealthTransfer.Instance;
                    equipedAbilityName = abilityID;
                    break;
                case "BubbleShield":
                    equipedAbility = BubbleShield.Instance;
                    equipedAbilityName = abilityID;
                    break;
                default:
                    print("failed to swap to: " + abilityID);
                    break;
            }
        }
        else
        {
            print("Failed to equip ability: Null ability");
        }
    }

    public void EquipPassive(string abilityID)
    {
        if (abilityID != null)
        {
            switch (abilityID)
            {
                case "HealForce":
                    passiveAbility = HealForcePassive.Instance;
                    passiveAbilityName = abilityID;
                    print("swap to: " + abilityID);
                    break;
                case "SolesOfTheDamned":
                    passiveAbility = SolesOfTheDamned.Instance;
                    passiveAbilityName = abilityID;
                    print("swap to: " + abilityID);
                    break;
                case "ZoneMomentum":
                    passiveAbility = ZoneMomentum.Instance;
                    passiveAbilityName = abilityID;
                    print("swap to: " + abilityID);
                    break;
                default:
                    print("failed to swap to: " + abilityID);
                    break;
            }
        }
    }
}