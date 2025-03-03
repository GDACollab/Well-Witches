using UnityEngine;

public class Warden_BigBlast : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject prefab;

    void OnActivateAbility()    // called by the Player Input component
    {
        BigBlast bb = Instantiate(prefab, spawnPoint.position, Quaternion.identity).GetComponent<BigBlast>();
        bb.Activate(duration);
    }
}
