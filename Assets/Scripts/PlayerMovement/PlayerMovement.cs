using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    Rigidbody2D rb;
    Vector2 moveDir;

    public enum playerType {WARDEN,GATHERER};
    [Header("Two Player Movement")]
    [SerializeField] public playerType movementType = playerType.GATHERER;

    [Header("References")]
    [SerializeField] public GameObject gatherer = null;
    [SerializeField] public SpringJoint2D springJoint = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb) Debug.LogError("Rigidbody2D not found! Please attach a Rigidbody2D to the GameObject.");
    }

    void Update()
    {
        if (movementType == playerType.WARDEN)
        {
            springJoint.connectedAnchor = gatherer.transform.InverseTransformPoint(gatherer.transform.position);
            springJoint.anchor = transform.InverseTransformPoint(transform.position);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed;
    }

    void OnMove(InputValue inputValue)
    {
        moveDir = inputValue.Get<Vector2>();
    }
}