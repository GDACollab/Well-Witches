using UnityEngine;
using Unity.Mathematics;

public class GathererHealthTransfer : GathererBaseAbilities
{

   public float temp; //hold the % of health from Singleton that holds that %value ex 0.25
   public float healthgate = 3; //temp to not let Gatherer Die from using this ability
   public float cooldownTime = 5f; //creates a five second cooldown (for testing)
   private float lastUsedTime;

    public override string abilityName => "HealthTransfer";
    public override float duration => cooldownTime;

    public static GathererHealthTransfer Instance { get; private set; }

    [Header("VFX Info")]
    public HealthTransfer VFX;
    public float delay;

    [SerializeField] private Transform wardenTransform;
    [SerializeField] private Transform gathererTransform;
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    [SerializeField] private float charge;
    public override float Charge
    {
        get => charge;
        set => charge = value;
    }

    void Awake()
    {
        InitSingleton();
        gathererTransform = transform;
        if (wardenTransform != null) { wardenTransform = GameObject.Find("Warden").transform; }
    }
    public override void useAbility()
   {
      if (Time.time > lastUsedTime + cooldownTime)
      { //varifies that someone isn't spamming the q button and there is a gap between presses (Abiltiy Cooldown)
         if (StatsManager.Instance.GathererCurrentHealth > healthgate)
         { //Ability confirmed to be Q && Timer
           // transfer health from Gatherer to Wanderer
            lastUsedTime = Time.time;

            // spawn vfx
            HealthTransfer healthTransfer = Instantiate(VFX, gathererTransform);
                StartCoroutine(healthTransfer.Initialize(wardenTransform, delay));


            temp = StatsManager.Instance.GathererCurrentHealth * StatsManager.Instance.healthTransferAmount; // temp holds %25 percent of Gatherer's current health
            
            EventManager.instance.playerEvents.PlayerDamage(math.round(temp), "Gatherer");
            StatsManager.Instance.WardenCurrentHealth += math.round(temp); //add to Wanderer Current Health

            if (StatsManager.Instance.WardenCurrentHealth > StatsManager.Instance.WardenMaxHealth)
            { 
               StatsManager.Instance.WardenCurrentHealth = StatsManager.Instance.WardenMaxHealth;
            }

            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.healthTransfer, this.transform.position);

            temp = 0f; //reset the health value stored (No longer needed health % can be different)
         }


      }
   }

}