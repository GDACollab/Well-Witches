using UnityEngine;

public class GathererHellfire : PassiveAbilities
{
    // Start is called before the first frame update
    public override string abilityName => "SolesOfTheDamned";
    [Header("Stats")]
    public float duration;
    public float damage;
    public float flamesSpawnedPerSecond;
    public float flameTicksPerSecond;
    public bool startAbility = false;
    public HellfireBooties fire;

    private float resetTimer;
    private float currentTimer;
    private Rigidbody2D rb;
    public static GathererHellfire Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    void Awake()
    {
        InitSingleton();
        rb = GetComponent<Rigidbody2D>();
    }
    //resetTimer is set so it creates n flames per second
    void Start()
    {
        resetTimer = 1f/flamesSpawnedPerSecond;
        currentTimer = resetTimer;
    }
    public override void passiveUpdate() 
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0f)
        {
            float angle = Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg;
            HellfireBooties shoes =  Instantiate(fire, transform.position, Quaternion.Euler(0, 0, -angle));
            shoes.Initialize(duration, damage, flameTicksPerSecond);
            currentTimer = resetTimer;
        }
    }
}
