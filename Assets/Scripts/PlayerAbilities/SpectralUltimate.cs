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

    [Header("Debug")]
    public float timeActive = 0f;
    [SerializeField] private GameObject pivotPoint;

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
        pivotPoint = new GameObject("SpectralPivotPoint");
    }

    private void Update()
    {
        pivotPoint.transform.position = transform.position;
        pivotPoint.transform.Rotate(Vector3.forward, projectileSpeed);

        // TODO:
        // tbh the instance should be deleted?
        // not sure how to turn off the ability yet for now this is one time use, change later
        // if the instance is deleted and respawned this'll work fine
        timeActive += Time.deltaTime;
        if (timeActive >= abilityDuration)
        {
            CancelInvoke();
            Destroy(pivotPoint);
        }
    }

    private void SpawnSpectralProjectile()
    {
        SpectralProjectile projectile = Instantiate(projectilePrefab, new Vector3(pivotPoint.transform.position.x, pivotPoint.transform.position.y + distanceFromPlayer, pivotPoint.transform.position.z), Quaternion.identity).GetComponent<SpectralProjectile>();
        projectile.Initialize(pivotPoint.transform, projectileDamage, projectileSpeed, projectileLifetime);
    }
}
