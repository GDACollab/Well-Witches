using UnityEngine;
using UnityEngine.InputSystem;

public class Gatherer_FlashStun : GathererBaseAbilities
{
    [SerializeField, Tooltip("Amount of time needed to get the ability off")] public float chargeDuration;
    [SerializeField, Tooltip("Radius of the circle that stuns enemies inside")] float radius = 16;  // 16 measured by me (Justin L) to go from left to right side of screen
    [SerializeField] float stunDuration;
    [field: SerializeField] public float cooldownDuration { get; private set; }
    [SerializeField] LayerMask collisionLayersToCheck;

    [Header("VFX Info")]
    [SerializeField] float startingVelocity;
    [SerializeField] float flashDuration;
    [SerializeField] float lifetime;



    InputAction activateAbilityAction;
    float chargeCounter;
    public float cooldownCounter { get; private set; } = 0;
    private bool canCastSpellSFX = false;

    public override string abilityName => "FlashStun";
    public override float duration => chargeDuration;

    public static Gatherer_FlashStun Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    [Header("References")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] FlashStun prefab;

    void Awake()
    {
        InitSingleton();
        activateAbilityAction = GetComponent<PlayerInput>().actions["Activate Ability"];
        chargeCounter = chargeDuration;
    }
    void Update()
    {
        if (cooldownCounter > 0)
        {
            cooldownCounter -= Time.deltaTime;
            return; // don't even think about charging up if on cooldown
        }
        if (!canCastSpellSFX)
        {
            canCastSpellSFX = true;
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.abilityReady, this.transform.position);
        }

        if (activateAbilityAction.IsPressed())
        {
            chargeCounter -= Time.deltaTime;
            //if (chargeCounter <= 0)
            //{
            //    chargeCounter = chargeDuration;
            //    cooldownCounter = cooldownDuration;
            //}
        }
        else chargeCounter = chargeDuration;

        //print(chargeCounter);
    }

    void ExecuteAbility()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.flashStun, this.transform.position);
        canCastSpellSFX = false;

        bool didHitEnemy = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, collisionLayersToCheck);
        foreach (Collider2D collider in colliders) if (collider.CompareTag("Enemy"))
            {
                if (!didHitEnemy) { didHitEnemy = true; }
                /* stun enemy */
            }

        if (didHitEnemy) { AudioManager.Instance.PlayOneShot(FMODEvents.Instance.flashStunHit, this.transform.position); }
    }

    public override void useAbility()
    {
        if (chargeCounter <= 0)
        {
            FlashStun flashStun = Instantiate(prefab, spawnPoint.position, Quaternion.identity).GetComponent<FlashStun>();
            flashStun.Initialize(startingVelocity, flashDuration, lifetime);
            ExecuteAbility();
            chargeCounter = chargeDuration;
            cooldownCounter = cooldownDuration;
        }
        else
        {
            print("dud: " + chargeCounter);
        }
    }
}