using UnityEngine;
using UnityEngine.InputSystem;

public class GathererBubbleShield : GathererBaseAbilities
{
    [field: SerializeField] public float cooldownDuration { get; private set; }
    public BubbleShield bubbleShieldPrefab;
    private InputAction activateAbilityAction;

    public override float duration => 10;
    public override string abilityName => "BubbleShield";
    public static GathererBubbleShield Instance { get; private set; }
	void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }

	void Awake()
	{
		InitSingleton();
        activateAbilityAction = GetComponent<PlayerInput>().actions["Activate Ability"];
    }

	public override void useAbility() // called by the GathererAbilityManager.cs
    {
        BubbleShield spellBurst = Instantiate(bubbleShieldPrefab, transform.position, Quaternion.identity).GetComponent<BubbleShield>();
    }
}
