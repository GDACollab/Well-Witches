using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WardenAbilityManager : MonoBehaviour
{
    private enum Abilities
    {
        DevastationBeam,
        GourdForge,
        SpellBurst,
    }

    public WardenBaseAbilities equipedAbility;
    [SerializeField] Abilities equipedAbilityName;
    public PassiveAbilities passiveAbility;
    [SerializeField] public string passiveAbilityName;
    [SerializeField] private Controls controls;

    //amount of times you kill an enemy for energy
    public float siphonTimes;
    public float waterDuration;
    public float waterSpeed;

    public static WardenAbilityManager Instance { get; private set; }
    public static Controls Controls {get => Instance.controls;}
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake() 
    { 
        InitSingleton();

        controls = new Controls();
        controls.Gameplay_Warden.Enable();
    }

    // Subscribe to the Warden controls input action asset "Activate Ability" action
    void OnEnable() 
    { 
        controls.Gameplay_Warden.ActivateAbility.performed += OnActivateAbility;
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    void OnDisable() 
    { 
        controls.Gameplay_Warden.ActivateAbility.performed -= OnActivateAbility;
        SceneManager.activeSceneChanged -= ChangedActiveScene;
    }


    private void Start()
    {
        if (equipedAbility == null || equipedAbility == null)
        {
            equipedAbility = WardenDevastationBeam.Instance;
            equipedAbilityName = Abilities.DevastationBeam;
        }
        EquipPassive("WaterLogging");
        Debug.Log(passiveAbilityName);
    }
    void OnActivateAbility(InputAction.CallbackContext context)
    {
        print("using warden ability");
        if (equipedAbility != null)
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
            case Abilities.DevastationBeam:
                equipedAbility = WardenDevastationBeam.Instance;
                break;
            case Abilities.GourdForge:
                equipedAbility = WardenGourdForge.Instance;
                break;
            case Abilities.SpellBurst:
                equipedAbility = WardenSpellBurst.Instance;
                break;
            default:
                print("failed to swap to: " + equipedAbilityName);
                break;
        }        
        
        if (passiveAbilityName != null)
        {
            switch (passiveAbilityName)
            {
                case "DeathDefy":
                    passiveAbility = AbilityDeathDefy.Instance;
                    break;
                case "SiphonEnergy":
                    passiveAbility = SiphonEnergy.Instance;
                    break;
                case "WaterLogging":
                    passiveAbility = WaterLogging.Instance;
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
                case "DevastationBeam":
                    equipedAbility = WardenDevastationBeam.Instance;
                    equipedAbilityName = Abilities.DevastationBeam;
                    break;
                case "GourdForge":
                    equipedAbility = WardenGourdForge.Instance;
                    equipedAbilityName = Abilities.GourdForge;
                    break;
                case "SpellBurst":
                    equipedAbility = WardenSpellBurst.Instance;
                    equipedAbilityName = Abilities.SpellBurst;
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
                case "DeathDefy":
                    passiveAbility = AbilityDeathDefy.Instance;
                    passiveAbilityName = abilityID;
                    print("swap to: " + abilityID);
                    break;
                case "SiphonEnergy":
                    passiveAbility = SiphonEnergy.Instance;
                    passiveAbilityName = abilityID;
                    print("swap to: " + abilityID);
                    break;
                case "WaterLogging":
                    passiveAbility = WaterLogging.Instance;
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
