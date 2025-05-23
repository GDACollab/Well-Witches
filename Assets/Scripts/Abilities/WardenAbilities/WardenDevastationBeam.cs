using System;
using UnityEngine;

public class WardenDevastationBeam : WardenBaseAbilities
{
    [field: Header("Charge")]
    [field: SerializeField] public int NumHitsRequired {  get; private set; }

    [Header("Damage")]
    [SerializeField] float damagePerTick;
    [SerializeField, Tooltip("in seconds")] float damageTickDuration;

    [Header("Knockback")]
    [SerializeField] float knockbackForce;
    [SerializeField, Tooltip("in seconds")] float knockbackTickDuration;

    [Header("Duration")]
    [SerializeField, Tooltip("in seconds")] float abilityDuration;

    [Header("References")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] DevastationBeam prefab;

    public override string abilityName => "DevastationBeam";
    public override int numHitsRequired => NumHitsRequired;
    public override float duration => abilityDuration;
    //changed charge to a float so I can add 3% of the total charge for each kill
    //also added Charge to WardenBaseAbilities so I can reference charge when saving the instance of the passive ability in a passiveabilities variable
    [SerializeField]private float charge;
    public override float Charge
    {
        get => charge;
        set => charge = value;
    }

    public static WardenDevastationBeam Instance { get; private set; } void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

	void Awake() { InitSingleton(); }
	void OnEnable() { PlayerProjectile.OnHitEnemy += GainCharge; }
	void OnDisable() { PlayerProjectile.OnHitEnemy -= GainCharge; }

    void GainCharge()
    {
        if (WardenAbilityManager.Instance.equipedAbility == this)
        {
            Charge++;
            if (Charge >= NumHitsRequired) { AudioManager.Instance.PlayOneShot(FMODEvents.Instance.abilityReady, this.transform.position); }
            if (Charge > NumHitsRequired) Charge = NumHitsRequired;
        }
            
    }

	public override void useAbility()    // called by the Ability Manager
    {
        Debug.Log("DevBeam HitsRequired: " + numHitsRequired);
        Debug.Log("DevBeam Charge: " + Charge);
        if (Charge < NumHitsRequired) return;
        DevastationBeam db = Instantiate(prefab, spawnPoint).GetComponent<DevastationBeam>();
        db.Activate(damagePerTick, damageTickDuration, knockbackForce, knockbackTickDuration, abilityDuration);
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.lazerFire, this.transform.position);
        Charge = 0f;
        StartCoroutine(CastSpell());
    }
}
