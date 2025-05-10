using UnityEngine;

public class SolesOfTheDamned : PassiveAbilities
{
    // Start is called before the first frame update
    public override string abilityName => "SolesOfTheDamned";
    [SerializeField]
    public float duration;
    public float damage;
    public float flamesSpawnedPerSecond;
    public float flameTicksPerSecond;
    public bool startAbility = false;
    public GameObject fire;

    private float resetTimer;
    private float currentTimer;
    public static SolesOfTheDamned Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

    void Awake()
    {
        InitSingleton();
    }
    //resetTimer is set so it creates n flames per second
    void Start()
    {
        resetTimer = 1f/flamesSpawnedPerSecond;
        currentTimer = resetTimer;
        //Instantiate(fire, transform.position, Quaternion.identity, transform);
    }
    public override void passiveUpdate() 
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0f)
        {
            Instantiate(fire, transform.position, Quaternion.identity, transform);
            currentTimer = resetTimer;
        }
    }

    //the code didnt work when it was in passiveUpdate() so I moved it to normal update
    //instatiates a fire object at Gatherer as a child object and resets the timer
    //public void Update()
    //{
    //    currentTimer -= Time.deltaTime;
    //    if (currentTimer <= 0f)
    //    {
    //        Instantiate(fire, transform.position, Quaternion.identity, transform);
    //        currentTimer = resetTimer;
    //    }
    //}
}
