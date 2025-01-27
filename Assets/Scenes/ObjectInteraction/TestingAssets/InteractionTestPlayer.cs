using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTestPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    
    Collider2D range;
    
    void Start()
    {
        range = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            tryInteract();
        }
    }

    void tryInteract() {
        
        List<Collider2D> colliderList = new List<Collider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        range.OverlapCollider(contactFilter.NoFilter(), colliderList);

        foreach(Collider2D collider in colliderList){

            if (collider.gameObject.TryGetComponent(out IInteractable interactableObject)) {
                interactableObject.Interact();
                break;
            }
        }

    }
}
