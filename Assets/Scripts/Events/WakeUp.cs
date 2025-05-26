using UnityEngine;

public class WakeUp : MonoBehaviour
{
    /// <summary>
    /// There is a prefab for this and it lets you show the player waking up.
    /// You need to position the player on the side of the bed for this to work.
    /// </summary>


    private bool toggleWakeUp = false;
    public Animator playerAnimator;
    public Animator canvasAnimator;
    public FirstPersonController player;

    void Start()
    {
        playerAnimator.enabled = true;
        canvasAnimator.SetTrigger("wakeUp");
        playerAnimator.SetBool("wakeUp", true);

        // Stop player from moving and interacting
        player.cameraCanMove = false;
        player.playerCanMove = false;
        player.enableHeadBob = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerAnimator != null)
        {
            if (!playerAnimator.GetBool("wakeUp") && !toggleWakeUp)
            {
                // Resume player movement and interacting
                playerAnimator.enabled = false;
                toggleWakeUp = true;
                player.cameraCanMove = true;
                player.playerCanMove = true;
                player.enableHeadBob = true;
            }
        }
    }


}