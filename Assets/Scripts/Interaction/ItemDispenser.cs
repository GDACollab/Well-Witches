using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDispenser : MonoBehaviour, IInteractable
{

    [SerializeField]
    private Item[] items;

    private SpriteRenderer spriteRenderer;
    private ParticleSystem particleSystem;

    private bool interacted = false;

    private float timer = 0f;
    [SerializeField] private float respawnTime = 30f;

    [SerializeField] private GameObject prefabToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
    }
    /*
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
    */
    void Dispense()
    {
        if (!interacted){
            int spawnX = Random.Range(-1, 2);
            int spawnY = 0;
            if(spawnX == 0) {
                spawnY = Random.Range(-1, 1) < 0 ? -1 : 1;
            } else {
                spawnY = Random.Range(-1, 2);
            }
            Item itemToDispense = ChooseItem();
            Debug.Log($"Dispensing item: {itemToDispense}");
            Instantiate(prefabToSpawn, new Vector3 (transform.position.x + spawnX,
                                            transform.position.y +spawnY, 0), Quaternion.identity);
            particleSystem.Stop();
            spriteRenderer.color = Color.grey;
            interacted = true;
            StartCoroutine(TrackTime());
        }
    }

    IEnumerator TrackTime()
    {
        // Wait for 30 seconds (Unity uses real-time seconds)
        yield return new WaitForSeconds(respawnTime);
        spriteRenderer.color = Color.green;

    }

    Item ChooseItem()
    {
        //using announcement system on pickup
        AnnouncementManager.Instance.AddAnnouncementToQueue("You got a thing!");
        return items[Random.Range(0, items.Length)];
    }

    void IInteractable.Interact()
    {
        Debug.Log("interaction!");
        Dispense();
    }
}
