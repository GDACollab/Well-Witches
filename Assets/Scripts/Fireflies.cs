using UnityEngine;

public class Fireflies : MonoBehaviour
{
    public Transform gatherer;

    private void Awake()
    {
        if (gatherer == null)
        {
            gatherer = GameObject.Find("Gatherer").transform;
        }
    }

    private void Update()
    {
        transform.position = gatherer.position;
    }
}
