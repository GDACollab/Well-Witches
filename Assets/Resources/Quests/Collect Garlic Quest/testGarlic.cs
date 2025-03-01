using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGarlic : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventManager.instance.miscEvent.GarlicCollected();
            Destroy(this.gameObject);
        }
    }
}
