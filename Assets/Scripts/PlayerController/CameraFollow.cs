using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Camera cameraToControl; // The camera that will follow this object
    public float smoothSpeed = 0.125f; // Speed of the smoothing effect
    public Vector3 offset; // Offset of the camera relative to this object

    void LateUpdate()
    {
        if (cameraToControl != null)
        {
            // Calculate the desired position with the offset
            Vector3 desiredPosition = transform.position + offset;

            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(cameraToControl.transform.position, desiredPosition, smoothSpeed);

            // Apply the smoothed position to the camera
            cameraToControl.transform.position = smoothedPosition;
        }
        else
        {
            Debug.LogError("Camera not assigned! Please assign a camera to control.");
        }
    }
}
