using UnityEngine;

public class GathererBubbleShield : GathererBaseAbilities
{
    [Header("Stats")]
    [SerializeField] private bool canUse = true;
    [field: SerializeField] public float cooldownDuration { get; private set; }

    [Header("References")]
    [SerializeField] BubbleShield bubbleShieldPrefab;

    private float timer;


    public override float duration => 10;
    public override string abilityName => "BubbleShield";
    public static GathererBubbleShield Instance { get; private set; }
	void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

	void Awake()
	{
		InitSingleton();
        timer = 0f;
    }

    private void Update()
    {
        if (!canUse && timer < cooldownDuration)
        {
            timer += Time.deltaTime;
            return;
        }
        else
        {
            canUse = true;
            timer = 0f;
        }
    }

    public override void useAbility() // called by the GathererAbilityManager.cs
    {
        if (canUse)
        {
            BubbleShield shield = Instantiate(bubbleShieldPrefab, transform).GetComponent<BubbleShield>();
            shield.Activate(duration);
            canUse = false;
        }
    }
}
