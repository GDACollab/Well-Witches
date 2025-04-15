using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WardenDisplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject wardenDisplay;
    private GameObject wardenRotatingDisplay;
    private GameObject warden;
    private SpriteRenderer wardenSprite;
    private Camera cam;
    [SerializeField]
    private int displayMargins = 10; // in pixels

    void Awake()
    {
        warden = GameObject.Find("Warden");
        cam = Camera.main;
        wardenRotatingDisplay = wardenDisplay.transform.GetChild(0).gameObject;
        wardenSprite = warden.GetComponentInChildren<SpriteRenderer>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        wardenDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // convert sprite size and position to viewport position (size is essentially just being rescaled)
        Vector3 spriteSize = cam.WorldToViewportPoint(cam.ViewportToWorldPoint(Vector3.zero) + wardenSprite.bounds.size);
        Vector3 spriteCenter = cam.WorldToViewportPoint(wardenSprite.bounds.center);
        
        // if sprite is off-screen, activate the display
        if (spriteCenter.x - spriteSize.x/2 > 1 || spriteCenter.x + spriteSize.x/2 < 0 ||
            spriteCenter.y - spriteSize.y/2 > 1 || spriteCenter.y + spriteSize.y/2 < 0)
        {
            wardenDisplay.SetActive(true);
        }
        else {
            wardenDisplay.SetActive(false);
        }

        // if/while its active, set position and rotation
        if (wardenDisplay.activeSelf) {
            // create margin size vector and convert to fraction of screen (viewport)
            Vector3 marginSize = new Vector3(displayMargins, displayMargins);
            marginSize = cam.ScreenToViewportPoint(marginSize); // convert pixels to fraction of screen

            // limit display position to within the screen margins
            float displayX = Mathf.Clamp(spriteCenter.x, 0 + marginSize.x, 1 - marginSize.x);
            float displayY = Mathf.Clamp(spriteCenter.y, 0 + marginSize.y, 1 - marginSize.y);

            // convert back to screen position
            Vector3 displayPosition = new Vector3(displayX, displayY);
            displayPosition = cam.ViewportToScreenPoint(displayPosition); // convert fraction of screen into pixels
            
            // set display position
            wardenDisplay.transform.position = displayPosition;

            // convert screen point into world point, to find angle
            displayPosition = cam.ScreenToWorldPoint(displayPosition);
            displayPosition.z = 0;
            
            // calculate angle between display and warden
            float angleToWarden = Vector3.SignedAngle(wardenSprite.bounds.center - displayPosition, Vector3.right, Vector3.back);
            angleToWarden += 90; // adjust to a downwards-facing arrow image

            // set rotation of the rotating part of the display
            wardenRotatingDisplay.transform.rotation = Quaternion.Euler(0, 0, angleToWarden);
        }
    }
}
