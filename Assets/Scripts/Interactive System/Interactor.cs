using System.Collections;
using UnityEngine;

//Notes: This is the script that goes with the player camera and
//will send the raycast for the interacting system.

public class Interactor : MonoBehaviour
{

    public Transform InteractorSource;
    public float InteractRange;

    private IInteractable currentInteractable;

    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                if (currentInteractable != interactObj)
                {
                    if (currentInteractable != null)
                    {
                        currentInteractable.Disappear_Key();
                    }

                    currentInteractable = interactObj;
                    currentInteractable.Appear_Key();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }
            }
            else
            {
                if (currentInteractable != null)
                {
                    currentInteractable.Disappear_Key();
                    currentInteractable = null;
                }
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                currentInteractable.Disappear_Key();
                currentInteractable = null;
            }
        }
    }
}

