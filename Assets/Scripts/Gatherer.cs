
using UnityEngine;

public class Gatherer : MonoBehaviour
{
    public FlashStunv1 FlashStun;
    public FlashStunv2 FlashStunv2;
    public FlashStunv3 FlashStunv3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            FlashStun.gameObject.SetActive(true);
            FlashStun.LaunchStun();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FlashStunv2.gameObject.SetActive(true);
            FlashStunv2.LaunchStun();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            FlashStunv3.gameObject.SetActive(true);
            FlashStunv3.LaunchStun();
        }
    }
}
