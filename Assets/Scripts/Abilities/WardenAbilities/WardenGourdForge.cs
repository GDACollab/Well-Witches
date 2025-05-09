using UnityEngine;

public class WardenGourdForge : WardenBaseAbilities
{
    [field: Header("Charge")]
    [field: SerializeField] public int NumHitsRequired { get; private set; }

    [Header("Damage")]
    [SerializeField] float damagePerTick;
    [SerializeField, Tooltip("in seconds")] float damageTickDuration;

    [Header("Size")]
    [SerializeField, Tooltip("Radius of AOE")] float size;

    [Header("Duration")]
    [SerializeField, Tooltip("in seconds")] float abilityDuration;

    [Header("References")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] GourdForge prefab;
    public override string abilityName => "GourdForge";
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

    public static WardenGourdForge Instance { get; private set; }
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

    public override void useAbility()    // called by the Player Input component
    {
        if (Charge < NumHitsRequired) return;
        GourdForge gourdForge = Instantiate(prefab, spawnPoint).GetComponent<GourdForge>();
        gourdForge.Activate(damagePerTick, damageTickDuration, size, abilityDuration);
        Charge = 0f;
    }
}
