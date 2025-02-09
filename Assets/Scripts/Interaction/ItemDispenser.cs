using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDispenser : MonoBehaviour, IInteractable
{

    [SerializeField]
    private Item[] items;

    private SpriteRenderer spriteRenderer;

    private float timer = 0f;
    [SerializeField] private float respawnTime = 30f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        Item itemToDispense = ChooseItem();

        Debug.Log($"Dispensing item: {itemToDispense}");
        spriteRenderer.color = Color.grey;
        StartCoroutine(TrackTime());

    }

    IEnumerator TrackTime()
    {
        // Wait for 30 seconds (Unity uses real-time seconds)
        yield return new WaitForSeconds(respawnTime);
        spriteRenderer.color = Color.green;

    }

    Item ChooseItem()
    {
        return items[Random.Range(0, items.Length)];
    }

    void IInteractable.Interact()
    {
        Dispense();
    }
}
