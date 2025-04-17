using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WardenAbilityManager : MonoBehaviour
{
    public WardenBaseAbilities equipedAbility;
    [SerializeField] private Controls controls;

    // WardenAbilityManager.Instance.equipedAbility.numHitsRequired
    public static WardenAbilityManager Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake() 
    { 
        InitSingleton();

        controls = new Controls();
        controls.Gameplay_Warden.Enable();
    }

    // Subscribe to the Warden controls input action asset "Activate Ability" action
    void OnEnable() { controls.Gameplay_Warden.ActivateAbility.performed += OnActivateAbility; }
    void OnDisable() { controls.Gameplay_Warden.ActivateAbility.performed -= OnActivateAbility; }


    private void Start()
    {
        equipedAbility = WardenDevastationBeam.Instance;
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

    /*
     TODO: Write a function that takes in an instance of an ability and sets "equipedAbility" to that instance
        will likley be called by whatever UI stuff selects abilities
     */
}
