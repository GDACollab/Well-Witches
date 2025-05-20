using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Collections;

public class Gatherer_FlashStun : GathererBaseAbilities
{
    [SerializeField, Tooltip("Amount of time needed to get the ability off")] public float chargeDuration;
    [SerializeField, Tooltip("Radius of the circle that stuns enemies inside")] float radius = 16;  // 16 measured by me (Justin L) to go from left to right side of screen
    // [SerializeField] float stunDuration;
    [field: SerializeField] public float cooldownDuration { get; private set; }
    [SerializeField] LayerMask collisionLayersToCheck;

    [Header("VFX Info")]
    [SerializeField] float startingVelocity;
    [SerializeField] float flashDuration;
    [SerializeField] float lifetime;



    InputAction activateAbilityAction;
    //float chargeCounter;
    public float cooldownCounter { get; private set; } = 0;
    private bool canCastSpellSFX = false;
    private NavMeshAgent navAgent;

    public override string abilityName => "FlashStun";
    public override float duration => chargeDuration;

    [SerializeField] private float charge;
    public override float Charge
    {
        get => charge;
        set => charge = value;
    }

    public static Gatherer_FlashStun Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    [Header("References")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] FlashStun prefab;

    void Awake()
    {
        InitSingleton();
        activateAbilityAction = GetComponent<PlayerInput>().actions["Activate Ability"];
        //chargeCounter = chargeDuration;
        // navAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        
        if (cooldownCounter > 0)
        {
            cooldownCounter -= Time.deltaTime;
            Charge = cooldownDuration - cooldownCounter;
            return; // don't even think about charging up if on cooldown
        }

        if (!canCastSpellSFX)
        {
            canCastSpellSFX = true;
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.abilityReady, this.transform.position);
        }
    }

    void ExecuteAbility()
    {
        canCastSpellSFX = false;

        bool didHitEnemy = false;

        // TODO: implement timer of length `lifetime` so the stun happens when the firework goes off

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, collisionLayersToCheck);
        foreach (Collider2D collider in colliders) if (collider.CompareTag("Enemy"))
            {
                if (!didHitEnemy) { didHitEnemy = true; }

                BaseEnemyClass enemy = null;
                if (collider.GetComponentInParent<MeleeEnemy>() != null) { enemy = collider.GetComponentInParent<MeleeEnemy>(); }
                else if (collider.GetComponentInParent<RangedEnemy>() != null) { enemy = collider.GetComponentInParent<RangedEnemy>(); }
                else if (collider.GetComponentInParent<TankEnemy>() != null) { enemy = collider.GetComponentInParent<TankEnemy>(); }

                if (enemy != null)
                {
                    enemy.getStunned();
                    Debug.Log("stunned this guy: " + enemy);
                }
            }

        if (didHitEnemy) 
        { 
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.flashStunHit, this.transform.position);
        }
        // for each enemy on screen, freeze them for 5 seconds,
        // by disabling their nav mesh agent or smth...
    }

    public override void useAbility()
    {
        if (cooldownCounter <= 0)
        {
            FlashStun flashStun = Instantiate(prefab, spawnPoint.position, Quaternion.identity).GetComponent<FlashStun>();
            flashStun.Initialize(startingVelocity, flashDuration, lifetime);
            // Debug.Log("launching firework");
            StartCoroutine(waitOutFireWork());
        }
    }

    IEnumerator waitOutFireWork()
    {
        cooldownCounter = cooldownDuration;
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.flashStun, this.transform.position);
        yield return new WaitForSeconds(lifetime);
        ExecuteAbility();
    }
}