using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Item item;


    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    void Pickup()
    {
        Debug.Log($"Picking up: {item}");

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.itemPickUp, this.transform.position);
        Destroy(gameObject);
    }

    void IInteractable.Interact()
    {
        Pickup();
    }
}
