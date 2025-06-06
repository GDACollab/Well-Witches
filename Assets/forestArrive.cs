using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestArrive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.forestArrive, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
