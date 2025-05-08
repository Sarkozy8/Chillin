using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

//Notes: This is the script that goes with the player camera and
//will send the raycast for the interacting system.

public class Interactor : MonoBehaviour
{

    public Transform InteractorSource;
    public float InteractRange;

    public FirstPersonController playerController; // Reference for player controller for enabling movement
    public Animator playerAnimator;                // Reference for Camera Animator
    public Rigidbody playerRB;                     // Used for animations and make them look smooth. Without it, I would have to code the animations andf I dont like doing animations :p
    public CapsuleCollider playerCollider;

    public Animator canvasAnimator;                // Used for Interact and display UI stuff
    public TextMeshProUGUI textForInteracts;       // Text component used for interacts
    protected TextMeshProUGUI textForTimer;        // Text Component used for the timers


    private IInteractable currentInteractable;

    private void Start()
    {

    }

    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                // Inject player into the interactable (if it's InteractBase class)
                if (interactObj is InteractBase baseInteractable)
                {
                    // This sends all the references needed to do animations, player controllers, and properly interact with objects
                    baseInteractable.SetReferences(playerController,
                                                   playerAnimator,
                                                   playerRB, playerCollider,
                                                   canvasAnimator,
                                                   textForInteracts,
                                                   textForTimer);
                }

                // Hides UI key when stops hovering
                if (currentInteractable != interactObj)
                {
                    if (currentInteractable != null)
                    {
                        currentInteractable.Disappear_Key();
                    }

                    // Shows UI key when hovering
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

