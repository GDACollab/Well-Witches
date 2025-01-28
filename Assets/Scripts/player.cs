using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horitzontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(horitzontal * speed, vertical * speed, 0f);
    }
}
