using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeControllerScript : MonoBehaviour
{
    private PlayerController_Warden pcw;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        pcw = collision.gameObject.GetComponent<PlayerController_Warden>();
        pcw.disableRope();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pcw = collision.gameObject.GetComponent<PlayerController_Warden>();
        pcw.enableRope();
    }
}
