using UnityEngine;

public class GathererHellfire : PassiveAbilities
{
    // Start is called before the first frame update
    public override string abilityName => "SolesOfTheDamned";
    [Header("Stats")]
    public float duration;
    public float damage;
    public float flameTicksPerSecond;
    public float flameSpawnDistance;
    public bool startAbility = false;
    public float offsetAmount;
    public HellfireBooties fire;

    private float distanceMoved;
    private Vector3 lastPos;
    private Rigidbody2D rb;
    private bool right;
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
        distanceMoved = 0;
        lastPos = transform.position;
    }
    public override void passiveUpdate() 
    {
        distanceMoved += Vector3.Distance(lastPos, transform.position);
        lastPos = transform.position;

        if (distanceMoved > flameSpawnDistance)
        {
            distanceMoved = 0;
            float angle = Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg;
            HellfireBooties shoes =  Instantiate(fire, transform.position, Quaternion.Euler(0, 0, -angle));
            shoes.Initialize(duration, damage, flameTicksPerSecond, right, offsetAmount);
            right = !right;
        }
    }
}
