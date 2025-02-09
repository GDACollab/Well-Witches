using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseTestThingRotator : MonoBehaviour
{
    private int degPerSec = 180;
    void Update()
    {
        transform.Rotate(0, 0, degPerSec * Time.deltaTime);
    }
}
