using System;
using UnityEngine;

public class WardenDevastationBeam : WardenBaseAbilities
{
    [field: Header("Charge")]
    [field: SerializeField] public int NumHitsRequired {  get; private set; }
    public int Charge { get; private set; } = 0;

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

    public static WardenDevastationBeam Instance { get; private set; } void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

	void Awake() { InitSingleton(); }
	void OnEnable() { PlayerProjectile.OnHitEnemy += GainCharge; }
	void OnDisable() { PlayerProjectile.OnHitEnemy -= GainCharge; }

    void GainCharge()
    {
        Charge++;
        if (Charge == NumHitsRequired) { AudioManager.Instance.PlayOneShot(FMODEvents.Instance.abilityReady, this.transform.position); }
        if (Charge > NumHitsRequired) Charge = NumHitsRequired;
    }

	public override void useAbility()    // called by the Ability Manager
    {
        if (Charge < NumHitsRequired) return;
        DevastationBeam db = Instantiate(prefab, spawnPoint).GetComponent<DevastationBeam>();
        db.Activate(damagePerTick, damageTickDuration, knockbackForce, knockbackTickDuration, abilityDuration);
        Charge = 0;
    }
}
