using System.Collections;
using UnityEngine;

public class Rocking_Chair_Interact : InteractBase
{

    private bool areWeInteracting = false; // Toggle for Interacting
    private bool areWeInteracting2 = false; // Toggle for Interacting and avoid double interaction on the same frame

    public BoxCollider chairCollider;      // Used so the player doesnt collide with chair when interacting
    public MeshRenderer chairRender;       // Used so the player camera doesnt clip into the chair

    void Update()
    {

        if (!areWeInteracting && !canInteract)
        {
            // Used to avoid being stuck in interact
            // It's position here because of the hierarchy on the interact function

            canInteract = true;
        }

        if (areWeInteracting && areWeInteracting2 && Input.GetKeyDown(KeyCode.E))
        {
            // Deinteract object logic
            playerAnimator.SetBool("interactWithRockingChair", false);
            chairRender.enabled = true;
        }

        if (playerAnimator != null)
        {
            if (playerAnimator.GetBool("endOfRockingChairTrigger"))
            {
                playerAnimator.SetBool("endOfRockingChairTrigger", false);
                player.cameraCanMove = true;
                player.playerCanMove = true;
                player.enableHeadBob = true;
                areWeInteracting = false;
                areWeInteracting2 = false;

                chairCollider.enabled = true;


                playerAnimator.enabled = false;

                // *****Deactivate music******
            }
        }

    }

    public override void Appear_Key()
    {
        Debug.Log("Interact Key Appear");
        textForInteracts.text = "Chill";
        canvasAnimator.SetBool("showKey", true);
    }

    public override void Disappear_Key()
    {
        Debug.Log("Interact Key Dissapear");
        canvasAnimator.SetBool("showKey", false);
    }

    public override void Interact()
    {
        if (areWeInteracting == false && canInteract == true) // Avoid getting stuck in interacting an object
        {

            Debug.Log("Interact with Rocking Chair");
            player.cameraCanMove = false;  // Stop player from moving
            player.playerCanMove = false;
            player.enableHeadBob = false;

            areWeInteracting = true;       // Enables interacting logic
            areWeInteracting2 = false;
            canInteract = false;           // Avoid further interacting until this interaction is done
            chairCollider.enabled = false; // Avoids clipping the chair

            playerAnimator.enabled = true; // Cant use animator and controller at the same time because of the rigidbody, that is why is disable when moving

            playerAnimator.SetBool("interactWithRockingChair", true);

            StartCoroutine(DisableChairRendererAndPLayMusic());
            Disappear_Key();
        }
    }

    IEnumerator DisableChairRendererAndPLayMusic()
    {
        // It is needed to deactivate chair renderer since it clips with camera when using

        yield return new WaitForSeconds(2f);

        chairRender.enabled = false;

        areWeInteracting2 = true;
        // *****and play music*****

    }


}
