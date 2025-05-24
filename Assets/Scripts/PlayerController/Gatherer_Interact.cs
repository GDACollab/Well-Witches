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
    [SerializeField] PlayerMovement myMovement;
    IInteractable interactable = null;
    public float timeSpent = 0.0f; //time spent searching
    public float timer = 1f; //time it takes to search
    
    private Animator animator;
    private CapsuleCollider2D playerCollider;
    private Vector3 oldPos;

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
                    //Freeze movement
                    myMovement.canMove = false;
                    //begin harvesting
                    interactable = interactableObject;
                    timeSpent = 0;
                    oldPos = transform.position;
                    StartAnimation(collider.transform.position);
                    break;
                }
            }
        }
        else if(iv.Get<float>() == 0) //just released
        {
            if (interactable != null)
            {
                //Unfreeze movement
                myMovement.canMove = true;
                //stop harvesting
                interactable = null;
                EndAnimation();
            }
        }
    }

    void Update()
    {
        timeSpent += Time.deltaTime;
        if (timeSpent > timer && interactable != null)
        {
            //Unfreeze movement
            myMovement.canMove = true;
            //stop harvesting
            interactable.Interact();
            interactable = null;
            EndAnimation();
        }
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    void StartAnimation(Vector3 cPos)
    {
        animator.SetTrigger("Gather");
        animator.SetBool("isGathering", true);
        playerCollider.excludeLayers = 0;
        transform.position = cPos;
    }
    
    void EndAnimation()
    {
        animator.SetBool("isGathering", false);
        playerCollider.excludeLayers = new LayerMask();
        transform.position = oldPos;
    }
}
