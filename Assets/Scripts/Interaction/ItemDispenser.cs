using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class ItemDispenser : MonoBehaviour, IInteractable
{
    private Item[] items;

    [Header("Bush Visuals")]
    [SerializeField] private Sprite activeBushSprite;
    [SerializeField] private Sprite inactiveBushSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    

    [Header("Bush Light")]
    [SerializeField] private Light2D light2D;
    [SerializeField] private float lightPulseSpeed;
    [SerializeField] private float lightMaxBrightness;

    private ParticleSystem particleSystem;

    public bool interacted = false;

    [Header("Bush Info")]
    [SerializeField] private float respawnTime;
    [SerializeField] private GameObject prefabToSpawn;

    private GameObject keyItemToSpawn;

    // Pre-Made list of (name, timer) tuples for both buffs and debuffs. 
    private List<(string, float)> possibleBuffs = new List<(string, float)> 
    {
        ("Speed Up!", 10f),
        ("Attack Up!", 8f), 
        ("Harvest Up!", 12f),
        ("Yank Up!", 10f), 
        ("Luck Up!", 30f), 
    };

    private List<(string, float)> possibleDebuffs = new List<(string, float)> 
    {
        ("Speed Down...", 10f), 
        ("Attack Down...", 8f), 
        ("Harvest Down...", 12f),
        ("Yank Down...", 10f), 
        ("Luck Down...", 30f),
    };


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
        keyItemToSpawn = Resources.Load<GameObject>("KeyItems/KeyItem");
        if (spriteRenderer == null) { spriteRenderer = GetComponentInChildren<SpriteRenderer>(); }
        if (light2D == null) { light2D = GetComponentInChildren<Light2D>(); }
    }

    private void Update()
    {
        if (!interacted)
        {
            light2D.pointLightInnerRadius = Mathf.PingPong(Time.time * lightPulseSpeed, lightMaxBrightness);
        }
    }

    void Dispense()
    {
        if (!interacted){ //Checks if the bush hasn't been interacted with before.



            // Finds a spot to spawn the item next to the bush.

            int spawnX = Random.Range(-1, 2);
            int spawnY = 0;
            if(spawnX == 0) {
                spawnY = Random.Range(-1, 1) < 0 ? -1 : 1;
            } else {
                spawnY = Random.Range(-1, 2);
            }

            /*
            Item itemToDispense = ChooseItem();
            Debug.Log($"Dispensing item: {itemToDispense}");
            */


            // Takes the active quest items dictionary and current buffs list from StatsManager
            GameObject questItem = GameManager.instance.activeQuestPrefab;
            List<string> currentBuffs = StatsManager.Instance.getMyBuffs();
            bool questActive = false;
            float totalQuestItemChance = 0.5f;
            if (questItem != null) {  //Checks if there are currently no active quest items
                questActive = true;
                prefabToSpawn = questItem;
            }
            // Takes a random float between [0, 1) (exclusive).
            float ROLL = Random.value;

            // Gets the current chance to spawn a key item from the StatsManager.
            float keyItemChance = StatsManager.Instance.getKeyItemChance();

            Debug.Log($"Key Item Chance: {keyItemChance}");

            Debug.Log($"First Round Roll: {ROLL}");

            // First checks if the ROLL is less than the keyItemChance. 
            // If it is, we spawn the key item and set the chance in the StatsManager back to 5%.
            // If not, then add 6% to the curent keyItemChance in StatsManager.
            if (ROLL <= keyItemChance) {
                Debug.Log("HIIII!!!!");
                GameObject keyItemDrop = Instantiate(
                    keyItemToSpawn,
                    new Vector3(transform.position.x + spawnX, transform.position.y +spawnY, 0f),
                    Quaternion.identity
                );
                KeyItem keyItemScript = keyItemDrop.GetComponent<KeyItem>();
                Debug.Log($"{GameManager.instance.currentKeyItem}");
                keyItemScript.keyItemID = GameManager.instance.currentKeyItem;
                keyItemScript.setSprite(keyItemScript.keyItemID);

                GameManager.instance.currentKeyItem++;
                StatsManager.Instance.keyItemChance = 0.05f;

                Debug.Log("Instantiated on Key Item!");
                Debug.Log("Vacating");
                vacate();
                return;
            } else {
                StatsManager.Instance.keyItemChance += 0.06f;
                Debug.Log($"Added to Key Item Chance: {StatsManager.Instance.getKeyItemChance()}");
            }


            // If we didn't roll to drop a key item, 
            // If there are questItems to drop, we check if the ROLL is less than the total questItemChance.
            // If yes, then we drop a questItem.
            // If not, we move on to apply a status.
            Debug.Log("qactive: " + questActive);
            if (questActive && ROLL <= totalQuestItemChance)
            {
                Debug.Log($"Now in Quest Items. Roll: {ROLL}");

                if (ROLL - keyItemChance <= totalQuestItemChance)
                {
                    Debug.Log("Rolled to drop a quest item!");
                    Instantiate(prefabToSpawn, new Vector3(transform.position.x + spawnX,
                                        transform.position.y + spawnY, 0), Quaternion.identity);
                    GameManager.instance.activeQuestItemCount--;
                    if (GameManager.instance.activeQuestItemCount <= 0)
                    {
                        //GameManager.instance.activeQuestPrefab = null;
                        GameManager.instance.activeQuestItemCount = 0;

                    }

                    Debug.Log("Vacating");
                    vacate();
                    return;


                }
            }

            // Now, we take a new ROLL and check if it's under a certain number.
            // This number is 40%, but since Luck is a thing that can be changed here.
            // If the ROLL is below the debuff benchmark, apply the debuff
            // Else, apply a buff.
            // applyStatus() adds the applied buff's name and timer to the myBuffs Dictionary.
            // applyStatus() takes in a currentBuffs list from StatsManager to prevent buff "stacking".
            ROLL = Random.value;
            Debug.Log($"Now in Status Roll: {ROLL}");
            if (ROLL <= 0.4f) {
                Debug.Log("Applying a debuff!");
                applyStatus(possibleDebuffs, currentBuffs);
            } else {
                Debug.Log("Applying a buff!");
                applyStatus(possibleBuffs, currentBuffs);
            }
            Debug.Log("Vacating");
            vacate();
        }
    }

    void applyStatus(List<(string, float)> toApply, List<string> currentBuffs) 
    {
        int ROLL;
        
        List<int> checkedIndices = new List<int>();

        int attempts = 0;

        while (attempts < toApply.Count) {
            ROLL = Random.Range(0, toApply.Count);
            if (!checkedIndices.Contains(ROLL)){
                if (!currentBuffs.Contains(toApply[ROLL].Item1)){
                    StatsManager.Instance.addStatus(toApply[ROLL].Item1, toApply[ROLL].Item2);
                    return;
                } else {
                    checkedIndices.Add(ROLL);
                    attempts++;
                }
            }
        }
        Debug.Log("No available buffs/debuffs to pick from");
    }

    void vacate() {
        particleSystem.Stop();
        spriteRenderer.sprite = inactiveBushSprite;
        interacted = true;
        light2D.enabled = false;
        StartCoroutine(RespawnBush());
    }

    IEnumerator RespawnBush()
    {
        yield return new WaitForSeconds(respawnTime);
        spriteRenderer.sprite = activeBushSprite;
        particleSystem.Play();
        light2D.enabled = true;
        interacted = false;
    }

    Item ChooseItem()
    {
        AnnouncementManager.Instance.AddAnnouncementToQueue("You got a thing!");
        return items[Random.Range(0, items.Length)];
    }

    void IInteractable.Interact()
    {
        Debug.Log("interaction!");
        Dispense();
    }
}
