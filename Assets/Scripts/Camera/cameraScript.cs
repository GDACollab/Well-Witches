using UnityEngine;

public class CameraScript : MonoBehaviour
{

    [SerializeField] private GameObject target;

    [Tooltip("Maximum coordinate the camera can move.")]
    [SerializeField] private Vector2 maxViewCoord;
    [Tooltip("Miniumm coordinate the camera can move.")]
    [SerializeField] private Vector2 minViewCoord;

    [Tooltip("How fast the camera lerps to the target.")]
    [SerializeField] private float cameraMoveSpeed = 2f;

    private float x;
    private float y;

    private void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -11);
    }

    private void Update()
    {
        //Clamp x and y positions based on min and max view coords
        x = target.transform.position.x;
        x = Mathf.Min(x, maxViewCoord.x);
        x = Mathf.Max(x, minViewCoord.x);

        y = target.transform.position.y;
        y = Mathf.Min(y, maxViewCoord.y);
        y = Mathf.Max(y, minViewCoord.y);

        //Lerp (slowly move) camera to these calculated x and y coordinates
        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, -11), Time.deltaTime * cameraMoveSpeed);
    }
}
