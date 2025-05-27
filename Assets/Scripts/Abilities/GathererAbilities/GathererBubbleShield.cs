using UnityEngine;

public class GathererBubbleShield : GathererBaseAbilities
{
    [field: SerializeField] public float cooldownDuration { get; private set; }
    public float abilityDuration;

    [Header("References")]
    [SerializeField] BubbleShield bubbleShieldPrefab;
    [SerializeField] Transform spawnPoint;

    [Header("Debug")]
    [SerializeField] private bool canUse = true;
    public bool isActive = false;

    public override float duration => cooldownDuration;
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
        if (!isActive && !canUse)
        {
            if (Charge < cooldownDuration)
            {
                Charge += Time.deltaTime;
            } else
            {
                canUse = true;
                AudioManager.Instance.PlayOneShot(FMODEvents.Instance.abilityReady, this.transform.position);
            }
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
            isActive = true;
        }
    }
}
