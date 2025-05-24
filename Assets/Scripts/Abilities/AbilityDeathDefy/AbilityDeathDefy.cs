using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDeathDefy : PassiveAbilities
{
    public override string abilityName => "DeathDefy";
    [SerializeField] private float totalTime = 45f; // CHANGE IF VALUE CHANGES FROM 45 SECONDS
    [Tooltip("Invincibility frames on revive, in seconds")]
    [SerializeField] private float invincibilityFrames = 1f;
    [Tooltip("Insert a prefab containing a sprite of the visual indicator when revived")]
    [SerializeField] private GameObject resurrectionVisual;
    private GameObject resurrectionReference;
    private PlayerHealth playerHealth;


    private float currentTime; // Respresents the time left in seconds on the timer
    private bool timerOn = false; // Tracks whether or not the timer is running
    private float healthIncrementValue; // The value to increment the HP by
    private bool hasTriggered = false;

    public static AbilityDeathDefy Instance { get; private set; }
    void InitSingleton() { if (Instance && Instance != this) Destroy(gameObject); else Instance = this; }
    void Awake()
    {
        InitSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthIncrementValue = StatsManager.Instance.WardenMaxHealth / totalTime; // Increments the HP over time with this value

        playerHealth = GetComponent<PlayerHealth>();
        if (resurrectionVisual != null)
        {
            Vector3 offset = new Vector3(0, 1.5f, 0);
            resurrectionReference = Instantiate(resurrectionVisual, transform.position + offset, Quaternion.identity);
            resurrectionReference.transform.SetParent(transform);
            resurrectionReference.SetActive(false);
        }else
        {
            Debug.Log("WARNING: Revive Indicator not set.");
        }

    }

    // Update is called once per frame
    public override void passiveUpdate()
    {
        if (!hasTriggered && StatsManager.Instance.WardenCurrentHealth <= 0 && timerOn == false) // Checks to see if Warden is at 0 HP
        {
            timerOn = true; // Sets the timer event to true
            currentTime = totalTime; // Sets the timer
            hasTriggered = true;
        }
        else if (timerOn)
        {
            EventManager.instance.playerEvents.DisableDamage(true);

            currentTime -= Time.deltaTime; // Decrements the timer

            // Increases Warden's HP over time
            StatsManager.Instance.WardenCurrentHealth += healthIncrementValue * Time.deltaTime;
            playerHealth.UpdateHealthBar(StatsManager.Instance.WardenCurrentHealth, StatsManager.Instance.WardenMaxHealth);


            if (currentTime <= 0f) // If the timer is up
            {
                if (resurrectionVisual != null)
                {
                    resurrectionReference.SetActive(true);
                    Invoke(nameof(TurnOffVisual), 1f);
                }

                currentTime = 0f; // Sets the time to 0
                timerOn = false; // Sets the timer event to false
                StatsManager.Instance.WardenCurrentHealth = StatsManager.Instance.WardenMaxHealth; // Guarantees Warden has their max HP
                WardenAbilityManager.Controls.Gameplay_Warden.Enable();
                GetComponentInChildren<Animator>().SetTrigger("Respawn");
                StartCoroutine(GiveCollisionBack());
            }
        }
    }

    void TurnOffVisual()
    {
        resurrectionReference.SetActive(false);
    }

    IEnumerator GiveCollisionBack()
    {
        yield return new WaitForSeconds(invincibilityFrames);

        EventManager.instance.playerEvents.DisableDamage(false);
    }
}
