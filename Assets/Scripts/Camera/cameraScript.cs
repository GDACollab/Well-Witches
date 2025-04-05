using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{

    [SerializeField] private GameObject target;

    [Tooltip("Maximum coordinate the camera can move.")]
    [SerializeField] private Vector2 maxViewCoord;
    [Tooltip("Miniumm coordinate the camera can move.")]
    [SerializeField] private Vector2 minViewCoord;

    private float x;
    private float y;

    private void Update()
    {
        transform.position = target.transform.position;

        x = transform.position.x;
        x = Mathf.Min(x, maxViewCoord.x);
        x = Mathf.Max(x, minViewCoord.x);

        y = transform.position.y;
        y = Mathf.Min(y, maxViewCoord.y);
        y = Mathf.Max(y, minViewCoord.y);

        transform.position = new Vector3(x, y, -11);
    }
}
