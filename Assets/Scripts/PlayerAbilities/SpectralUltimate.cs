using UnityEngine;


public class SpectralUltimate : MonoBehaviour
{
    [field: Header("Charge")]
    [field: SerializeField] public int NumHitsRequired { get; private set; }
    public int Charge { get; private set; } = 0;

    [Header("Projectile Stats")]
    public float projectileDamage;
    public float projectileSpeed;
    public float distanceFromPlayer;
    public float projectileLifetime;


    [Header("Duration")]
    [SerializeField, Tooltip("in seconds")] float abilityDuration;

    [Header("References")]
    public SpectralProjectile projectilePrefab;
    public Transform pivot;

    [Header("Debug")]
    public float timeActive = 0f;

    // this stuff should prob be moved into wardenbaseability or something
    public static SpectralUltimate Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake() { InitSingleton(); }
    //void OnEnable() { PlayerProjectile.OnHitEnemy += GainCharge; }
    // void OnDisable() { PlayerProjectile.OnHitEnemy -= GainCharge; }

    void Start()
    {
        InvokeRepeating(nameof(SpawnSpectralProjectile), 0, 0.5f);
        timeActive = 0f;
    }

    private void Update()
    {
        pivot.Rotate(Vector3.forward, projectileSpeed);
        timeActive += Time.deltaTime;
        if (timeActive >= abilityDuration)
        {
            CancelInvoke();
        }
    }

    private void SpawnSpectralProjectile()
    {
        SpectralProjectile projectile = Instantiate(projectilePrefab, new Vector3(pivot.position.x, pivot.position.y + distanceFromPlayer, pivot.position.z), Quaternion.identity).GetComponent<SpectralProjectile>();
        projectile.Initialize(pivot, projectileDamage, projectileSpeed, distanceFromPlayer, projectileLifetime);
    }
}
