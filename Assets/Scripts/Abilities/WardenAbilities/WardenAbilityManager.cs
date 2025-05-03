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
    [SerializeField] private Controls controls;

    public static WardenAbilityManager Instance { get; private set; }
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
    }
    void OnActivateAbility(InputAction.CallbackContext context)
    {
        print("using warden ability");
        if (equipedAbility != null)
        {
            equipedAbility.useAbility();
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
    //public void EquipActive(string abilityID)
    //{
        // the same as above, just for passives (look at gatherer manager)
    //}
}
