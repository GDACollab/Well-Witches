using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GathererAbilityManager : MonoBehaviour
{
    private enum Active
    {
        SolarFlare,
        SharingIsCaring,
        BubbleBarrier
    }

    private enum Passive
    {
        AloeVera,
        HellfireBootie,
        Espresso
    }

    public GathererBaseAbilities equipedAbility;
    [SerializeField] Active equipedAbilityName;
    public PassiveAbilities passiveAbility;
    [SerializeField] Passive passiveAbilityName;
    [SerializeField] private Controls controls;

    //Public functions to get strings incase its relevant
    public string GetEquippedActiveName()
    {
        return equipedAbilityName.ToString();
    }

    public string GetEquippedPassiveName()
    {
        return passiveAbilityName.ToString();
    }

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
        if (equipedAbility == null)
        {
            equipedAbility = Gatherer_FlashStun.Instance;
            equipedAbilityName = Active.SolarFlare;
        }
        if (passiveAbility == null)
        {
            passiveAbility = HealForcePassive.Instance;
            passiveAbilityName = Passive.AloeVera;
        }
    }
    void OnActivateAbility(InputAction.CallbackContext context)
    {
        print("using gatherer ability");
        if (equipedAbility != null)
        {
            equipedAbility.useAbility();
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
        switch (equipedAbilityName)
        {
            case Active.SolarFlare:
                equipedAbility = Gatherer_FlashStun.Instance;
                break;
            case Active.SharingIsCaring:
                equipedAbility = GathererHealthTransfer.Instance;
                break;
            case Active.BubbleBarrier:
                equipedAbility = GathererBubbleShield.Instance;
                break;
            default:
                print("failed to swap to: " + equipedAbilityName);
                break;
        }

        // needs to be looked into 
        switch (passiveAbilityName)
        {
            case Passive.AloeVera:
                passiveAbility = HealForcePassive.Instance;
                break;
            case Passive.HellfireBootie:
                passiveAbility = SolesOfTheDamned.Instance;
                break;
            case Passive.Espresso:
                passiveAbility = ZoneMomentum.Instance;
                break;
            default:
                print("failed to swap to: " + passiveAbilityName);
                break;
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
                    equipedAbilityName = Active.SolarFlare;
                    break;
                case "HealthTransfer":
                    equipedAbility = GathererHealthTransfer.Instance;
                    equipedAbilityName = Active.SharingIsCaring;
                    break;
                case "BubbleShield":
                    equipedAbility = GathererBubbleShield.Instance;
                    equipedAbilityName = Active.BubbleBarrier;
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
                    passiveAbilityName = Passive.AloeVera;
                    print("swap to: " + abilityID);
                    break;
                case "SolesOfTheDamned":
                    passiveAbility = SolesOfTheDamned.Instance;
                    passiveAbilityName = Passive.HellfireBootie;
                    print("swap to: " + abilityID);
                    break;
                default:
                    print("failed to swap to: " + abilityID);
                    break;
            }
        }
    }
}