using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WardenAbilityManager : MonoBehaviour
{
    private enum Active
    {
        DevastationBeam,
        GourdForge,
        SpellBurst,
    }

    public enum Passive
    {
        None,
        ResurrectionRegalia,
        SoulSiphon,
        BoggyBullets,
    }

    public WardenBaseAbilities equipedAbility;
    [SerializeField] Active equipedAbilityName;
    public PassiveAbilities passiveAbility;
    public Passive passiveAbilityName;
    [SerializeField] private Controls controls;

    //checks if waterlogging is active
    public float waterDuration;
    public float waterSpeed;

    public static WardenAbilityManager Instance { get; private set; }
    public static Controls Controls {get => Instance.controls;}
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    //Public functions to get names of abilities in case its relevant
    //For certain abilities
    public string GetEquippedActiveName()
    {
        return equipedAbilityName.ToString();
    }

    public string GetEquippedPassiveName()
    {
        return passiveAbilityName.ToString();
    }
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
        if (equipedAbility == null)
        {
            equipedAbility = WardenDevastationBeam.Instance;
            equipedAbilityName = Active.DevastationBeam;
        }
        if (passiveAbility == null)
        {
            passiveAbility = null;
            passiveAbilityName = Passive.None;
        }
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
            case Active.DevastationBeam:
                equipedAbility = WardenDevastationBeam.Instance;
                break;
            case Active.GourdForge:
                equipedAbility = WardenGourdForge.Instance;
                break;
            case Active.SpellBurst:
                equipedAbility = WardenSpellBurst.Instance;
                break;
            default:
                break;
        }        

        switch (passiveAbilityName)
        {
            case Passive.ResurrectionRegalia:
                passiveAbility = AbilityDeathDefy.Instance;
                break;
            case Passive.SoulSiphon:
                passiveAbility = SiphonEnergy.Instance;
                break;
            case Passive.BoggyBullets:
                passiveAbility = WaterLogging.Instance;
                break;
            default:
                break;
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
                    equipedAbilityName = Active.DevastationBeam;
                    break;
                case "GourdForge":
                    equipedAbility = WardenGourdForge.Instance;
                    equipedAbilityName = Active.GourdForge;
                    break;
                case "SpellBurst":
                    equipedAbility = WardenSpellBurst.Instance;
                    equipedAbilityName = Active.SpellBurst;
                    break;
                default:
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
                case "ResurrectionRegalia":
                    passiveAbility = AbilityDeathDefy.Instance;
                    passiveAbilityName = Passive.ResurrectionRegalia;
                    print("swap to: " + abilityID);
                    break;
                case "SoulSiphon":
                    passiveAbility = SiphonEnergy.Instance;
                    passiveAbilityName = Passive.SoulSiphon;
                    print("swap to: " + abilityID);
                    break;
                case "BoggyBullets": //Waterlogging script
                    passiveAbility = WaterLogging.Instance; //Script was called WaterLogging, as it was made before the name change
                    passiveAbilityName = Passive.BoggyBullets;
                    print("swap to: " + abilityID);
                    break;
                default:
                    print("failed to swap to: " + abilityID);
                    break;
            }
        }
    }
}
