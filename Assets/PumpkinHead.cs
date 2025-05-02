using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinHead : MonoBehaviour
{
    //This function is called when the player touches the pumpkin, they collect it and the object disappears
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventManager.instance.miscEvent.PumpkinCollected();
            
            
        }
    }
}
