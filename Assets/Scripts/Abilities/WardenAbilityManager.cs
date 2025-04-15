using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WardenAbilityManager : MonoBehaviour
{
    public WardenBaseAbilities equipedAbility;
    [SerializeField] private PlayerInput WardenControls;

    public static WardenAbilityManager Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake() { InitSingleton(); }

    void OnEnable() { PlayerProjectile.OnHitEnemy += OnActivateAbility; }
    void OnDisable() { PlayerProjectile.OnHitEnemy -= OnActivateAbility; }


    private void Start()
    {
        equipedAbility = WardenDevastationBeam.Instance;
        print("equip");
    }
    void OnActivateAbility()
    {
        print("using ability");
        equipedAbility.useAbility();
    }
}
