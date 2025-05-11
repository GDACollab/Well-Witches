using UnityEngine;

public class GathererBubbleShield : GathererBaseAbilities
{
    [Header("Stats")]
    [SerializeField] private bool canUse = true;
    [field: SerializeField] public float cooldownDuration { get; private set; }

    [Header("References")]
    [SerializeField] BubbleShield bubbleShieldPrefab;
    [SerializeField] Transform spawnPoint;

    private float timer;


    public override float duration => 10;
    public override string abilityName => "BubbleShield";
    public static GathererBubbleShield Instance { get; private set; }

    [SerializeField] private float charge;
    public override float Charge
    {
        get => charge;
        set => charge = value;
    }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

	void Awake()
	{
		InitSingleton();
        Charge = 0f;
    }

    private void Update()
    {
        if (!canUse && Charge < cooldownDuration)
        {

            Charge += Time.deltaTime;
            return;
        }
        else
        {
            canUse = true;
            //Charge = 0f;
        }
    }

    public override void useAbility() // called by the GathererAbilityManager.cs
    {
        if (canUse)
        {
            BubbleShield shield = Instantiate(bubbleShieldPrefab, spawnPoint).GetComponent<BubbleShield>();
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.bubbleActivate, this.transform.position);
            shield.Activate(duration);
            Charge = 0f;
            canUse = false;
        }
    }
}
