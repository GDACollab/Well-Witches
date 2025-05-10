using UnityEngine;


public class WardenSpellBurst : WardenBaseAbilities
{
    [field: Header("Charge")]
    [field: SerializeField] public int NumHitsRequired { get; private set; }

    [Header("Projectile Stats")]
    public float projectileDamage;
    public float projectileSpeed;
    public float projectileRotationForce;
    public float projectileLifetime;
    public float timeBetweenProjectile;
    public int projectilePerBurst;

    [Header("Duration")]
    [SerializeField, Tooltip("in seconds")] float abilityDuration;

    [Header("References")]
    public SpellBurstProjectile projectilePrefab;

    [Header("References")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] SpellBurst prefab;

    public override string abilityName => "SpellBurst";
    public override int numHitsRequired => NumHitsRequired;
    public override float duration => abilityDuration;
    //changed charge to a float so I can add 3% of the total charge for each kill
    //also added Charge to WardenBaseAbilities so I can reference charge when saving the instance of the passive ability in a passiveabilities variable
    [SerializeField] private float charge;
    public override float Charge
    {
        get => charge;
        set => charge = value;
    }
    public static WardenSpellBurst Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake() { InitSingleton(); }
    void OnEnable() { PlayerProjectile.OnHitEnemy += GainCharge; }
    void OnDisable() { PlayerProjectile.OnHitEnemy -= GainCharge; }

    void GainCharge()
    {
        Charge++;
        if (Charge >= NumHitsRequired) { AudioManager.Instance.PlayOneShot(FMODEvents.Instance.abilityReady, this.transform.position); }
        if (Charge > NumHitsRequired) Charge = NumHitsRequired;
    }

    public override void useAbility()    // called by the Ability Manager
    {
        if (Charge < NumHitsRequired) return;
        SpellBurst spellBurst = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<SpellBurst>();
        spellBurst.Activate(projectileDamage, projectileSpeed, projectileRotationForce, projectileLifetime, timeBetweenProjectile, projectilePerBurst, abilityDuration);
        Charge = 0f;
    }
}
