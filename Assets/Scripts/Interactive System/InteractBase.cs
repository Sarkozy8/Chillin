using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

//  Note: This is a "Base Class" where values needed between interact are kept.
//  Remember that these values get reset by scenes and not saved through them.
//  The interface "IInteractable" is used to make the "template" for the interacts. 

public abstract class InteractBase : MonoBehaviour, IInteractable
{

    protected FirstPersonController player; // Reference to modify player controller on all interacts using "player"
    protected Animator playerAnimator;      // Reference for animator on all interacts (for camera)
    protected Rigidbody playerRB;
    protected CapsuleCollider playerCollider;
    protected Animator canvasAnimator;
    protected TextMeshProUGUI textForInteracts;
    protected TextMeshProUGUI textForTimer;

    protected bool canInteract = true;

    public abstract void Appear_Key();

    public abstract void Disappear_Key();

    public abstract void Interact();

    public virtual void SetReferences(FirstPersonController playerRef,
                                      Animator animatorRef,
                                      Rigidbody playerRBRef,
                                      CapsuleCollider playerColliderRef,
                                      Animator canvasAnimatorRef,
                                      TextMeshProUGUI textForInteractsRef,
                                      TextMeshProUGUI textForTimerRef)
    {
        // Sets for all childs "player" as the player controller
        // Lets you mainly enable/disable player movement
        // Also sets canvas for interactors

        player = playerRef;
        playerAnimator = animatorRef;
        playerRB = playerRBRef;
        playerCollider = playerColliderRef;
        canvasAnimator = canvasAnimatorRef;
        textForInteracts = textForInteractsRef;
        textForTimer = textForTimerRef;

    }

    public virtual void SetAnimator()
    {
        // Sets for all childs "playerAnimator" as the player animator
        // Lets you actiavte animations on any interact


    }

}
