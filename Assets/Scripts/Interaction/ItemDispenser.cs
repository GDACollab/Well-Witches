using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDispenser : MonoBehaviour, IInteractable
{

    [SerializeField]
    private Item[] items;
    
    /* // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    } */

    void Dispense()
    {
        Item itemToDispense = ChooseItem();

        Debug.Log($"Dispensing item: {itemToDispense}");


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
