using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

public class WardenAbilityManager : MonoBehaviour
{
    public WardenBaseAbilities equipedAbility;
    [SerializeField] string equipedAbilityName;
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
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }


    private void Start()
    {
        equipedAbility = WardenGourdForge.Instance;
        equipedAbilityName = equipedAbility.abilityName;
        print("Ability is of type: " + equipedAbilityName);
        //print("equip");
    }
    void OnActivateAbility(InputAction.CallbackContext context)
    {
        print("using warden ability");
        if (equipedAbility != null)
        {
            equipedAbility.useAbility();
        }
    }

    void ChangedActiveScene(Scene current, Scene next)
    {
        if (equipedAbilityName != null)
        {
            switch (equipedAbilityName)
            {
                case "DevastationBeam":
                    equipedAbility = WardenDevastationBeam.Instance;
                    break;
                case "SpellBurst":
                    equipedAbility = WardenSpellBurst.Instance;
                    break;
                case "GourdForge":
                    equipedAbility = WardenGourdForge.Instance;
                    break;
                default:
                    print("failed to swap to: " + equipedAbilityName);
                    break;
            }
        }
        
    }

    /*
     TODO: Write a function that takes in an instance of an ability and sets "equipedAbility" to that instance
        will likley be called by whatever UI stuff selects abilities
     */
}
