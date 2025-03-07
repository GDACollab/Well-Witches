using System;
using UnityEngine;

public class Warden_BigBlast : MonoBehaviour
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
    [SerializeField] GameObject prefab;

	public static Warden_BigBlast Instance { get; private set; } void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

	void Awake() { InitSingleton(); }
	void OnEnable() { PlayerProjectile.OnHitEnemy += GainCharge; }
	void OnDisable() { PlayerProjectile.OnHitEnemy -= GainCharge; }

    void GainCharge()
    {
        Charge++;
        if (Charge == NumHitsRequired) { AudioManager.Instance.PlayOneShot(FMODEvents.Instance.abilityReady, this.transform.position); }
        if (Charge > NumHitsRequired) Charge = NumHitsRequired;
    }

	void OnActivateAbility()    // called by the Player Input component
    {
        if (Charge < NumHitsRequired) return;

        BigBlast bb = Instantiate(prefab, spawnPoint).GetComponent<BigBlast>();
        bb.Activate(damagePerTick, damageTickDuration, knockbackForce, knockbackTickDuration, abilityDuration);
        Charge = 0;
    }
}
