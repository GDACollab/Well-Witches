using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    Rigidbody2D rb;
    Vector2 moveDir;
    
    [Header("Item Pick Up")]
    Collider2D pickUpRange;

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

        if (movementType == playerType.GATHERER)
        {
            pickUpRange = GetComponent<Collider2D>();
            if (!pickUpRange) Debug.LogError("Collider2D not found! Please attach a Collider2D to the GameObject.");
        }
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

    void OnPickUpItem()
    {
        // copied code from InteractionTestPlayer.cs
        List<Collider2D> colliderList = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        pickUpRange.OverlapCollider(contactFilter.NoFilter(), colliderList);

        foreach(Collider2D collider in colliderList){
            if (collider.gameObject.TryGetComponent(out IInteractable interactableObject)) {
                interactableObject.Interact();
                break;
            }
        }
    }
}