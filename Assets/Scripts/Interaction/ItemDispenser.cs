using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ItemDispenser : MonoBehaviour, IInteractable
{

    [SerializeField]
    private Item[] items;

    private SpriteRenderer spriteRenderer;
    private ParticleSystem particleSystem;

    private bool interacted = false;

    private float timer = 0f;

    [SerializeField] private float respawnTime = 10f;

    [SerializeField] private GameObject prefabToSpawn;

    private GameObject keyItemToSpawn;


    // Pre-Made list of (name, timer) tuples for both buffs and debuffs. 
    private List<(string, float)> possibleBuffs = new List<(string, float)> 
    {
        ("SpeedUp", 10f),
        ("AttackUp", 8f), 
        ("HarvestUp", 12f),
        ("YankUp", 10f), 
        ("LuckUp", 30f), 
    };

    private List<(string, float)> possibleDebuffs = new List<(string, float)> 
    {
        ("SpeedDown", 10f), 
        ("AttackDown", 8f), 
        ("HarvestDown", 12f),
        ("YankDown", 10f), 
        ("LuckDown", 30f),
    };


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
        keyItemToSpawn = Resources.Load<GameObject>("Assets/Resources/KeyItems/KeyItem");
    }

    /*
    // Update is called once per frame
    void Update()
    {

    }
    */

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
            Dictionary<string, float> questItems = StatsManager.Instance.getQuestItems();
            List<string> currentBuffs = StatsManager.Instance.getMyBuffs();
            bool questActive = false;
            float totalQuestItemChance = 0;
            if (questItems.Count > 0) {  //Checks if there are currently no active quest items
                questActive = true;
                foreach (KeyValuePair<string, float> item in questItems){
                    totalQuestItemChance += (float)item.Value;
                }
                Debug.Log($"Total Quest Item Chance: {totalQuestItemChance}");
            }

            Debug.Log($"Quest Items Count: {questItems.Count}");

            // Takes a random float between [0, 1) (exclusive).
            float ROLL = Random.value;

            // Gets the current chance to spawn a key item from the StatsManager.
            float keyItemChance = StatsManager.Instance.getKeyItemChance();

            Debug.Log($"Key Item Chance: {keyItemChance}");

            Debug.Log($"First Round Roll: {ROLL}");

            // First checks if the ROLL is less than the keyItemChance. 
            // If it is, we spawn the key item and set the chance in the StatsManager back to 5%.
            // If not, then add 6% to the curent keyItemChance in StatsManager.
            if (ROLL <= 2f) {
                Debug.Log("HIIII!!!!");

                KeyItem keyItemScript = keyItemToSpawn.GetComponent<KeyItem>();
                keyItemScript.keyItemID = GameManager.instance.currentKeyItem;
                keyItemScript.setSprite(keyItemScript.keyItemID);

                Instantiate(
                    keyItemToSpawn,
                    new Vector3(transform.position.x + spawnX, transform.position.y +spawnY, 0f),
                    Quaternion.identity
                );

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
            if (questActive && ROLL <= totalQuestItemChance) {
                Debug.Log($"Now in Quest Items. Roll: {ROLL}");
                float questItemRunningTotal = 0f;
                foreach (KeyValuePair<string, float> item in questItems){
                    Debug.Log($"Running Total: {questItemRunningTotal}");
                    if (ROLL - keyItemChance <= item.Value + questItemRunningTotal) {
                        Debug.Log("Rolled to drop a quest item!");
                        Instantiate(prefabToSpawn, new Vector3 (transform.position.x + spawnX,
                                            transform.position.y +spawnY, 0), Quaternion.identity);
                        StatsManager.Instance.questItems.Remove(item.Key);
                        Debug.Log($"Removed Quest Item: {item.Key}");
                        Debug.Log("Vacating");
                        vacate();
                        return;
                    }
                    questItemRunningTotal += item.Value;
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

            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.interactBush, this.transform.position);
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

    IEnumerator TrackTime()
    {
        Debug.Log("BEFORE!!!!");
        // Wait for respawnTime (Unity uses real-time seconds)
        yield return new WaitForSeconds(respawnTime);

        Debug.Log("HIIII!!!");
        spriteRenderer.color = Color.green;
        particleSystem.Play();
        interacted = false;
    }

    void vacate() {
        particleSystem.Stop();
        spriteRenderer.color = Color.grey;
        interacted = true;
        StartCoroutine(TrackTime());
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
