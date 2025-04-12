using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gatherer_Interact : MonoBehaviour
{
    //Apologies if the code is a mess!
    //We don't quite understand the input system; if you feel you have a good grasp at it, take a shot.
    //Otherwise, you might have a bit of trouble...
    //Feel free to message me! (Ashton Gallistel, Eric Lien) I'll try to explain what I understand.
    //Major apologies for using a Vector2D in the editor, getting a float in the function, and treating it as an int in the code.
    [SerializeField] Collider2D interactionRadius;
    IInteractable interactable = null;
    public float timeSpent = 0.0f; //time spent searching
    public float timer = 1f; //time it takes to search

    void OnInteract(InputValue iv)   // called by the Player Input component
    {
        //iv's float = 1 means start pressing, 0 means stop
        //Debug.Log("Bluh A");
        //Debug.Log(iv.Get<float>()); //Don't worry about it, it just works

        if (iv.Get<float>() == 1) //just pressed
        {
            List<Collider2D> colliderList = new List<Collider2D>();
            ContactFilter2D contactFilter = new ContactFilter2D();
            interactionRadius.OverlapCollider(contactFilter.NoFilter(), colliderList);

            foreach (Collider2D collider in colliderList)
            {
                if (collider.gameObject.TryGetComponent(out IInteractable interactableObject))
                {
                    interactable = interactableObject;
                    timeSpent = 0;
                    break;
                }
            }
        }
        else if(iv.Get<float>() == 0) //just released
        {
            if (interactable != null)
            {
                interactable = null;
            }
        }
    }

    void Update()
    {
        timeSpent += Time.deltaTime;
        if (timeSpent > timer && interactable != null)
        {
            interactable.Interact();
            interactable = null;
        }
    }
}
