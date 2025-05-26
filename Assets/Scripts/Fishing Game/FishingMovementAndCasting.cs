using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingMovementAndCasting : MonoBehaviour
{
    /// <summary>
    /// Holds logic for player camera movement and fishing minigame logic.
    /// The player will cast 3 times and after the third time, if he clicks enoigh time, will get the fish.
    /// </summary>

    public Transform rodTip;
    public Transform bait;
    private LineRenderer lineRenderer;

    public float mouseSensitivity = 100f;
    public float rotationX = 0f;
    public float rotationY = 0f;

    private Camera cam;
    private Animator animator;

    private bool canICast = false;
    private bool canIReel = false;
    private int clickCounter = 0; // Counts click so it make sure that the player did something to reel the rod
    private bool gotFish = false;

    private SpringJoint joint;

    public Rigidbody rbBait;
    public Animator baitAnimator;
    public Animator canvasAnimator;
    public Animator canvasShowFishAnimator;

    private int counterOfCasting = 0;


    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("This script must be attached to a Camera.");
            enabled = false;
            return;
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Script didnt get the Animator");
            enabled = false;
            return;
        }

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("Script didnt get the lineRenderer");
            enabled = false;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.eulerAngles;
        rotationY = angles.y;
        rotationX = angles.x;

        canICast = true;
    }

    void Update()
    {

        // Mouse movement to rotate camera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;

        // Clamp the angles
        rotationX = Mathf.Clamp(rotationX, -20f, 10f);
        rotationY = Mathf.Clamp(rotationY, -53f, 0f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // Lets the player cast the rod if click the screen
        if (canICast && Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(CastRod());
        }

        if (canIReel && Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(ReelRod());
        }

        if (!canIReel && !canICast && Input.GetKeyDown(KeyCode.Mouse0))
        {
            clickCounter++;
        }

        // Logic for Line Renderer
        lineRenderer.SetPosition(0, rodTip.position);
        lineRenderer.SetPosition(1, bait.position);

    }



    IEnumerator CastRod()
    {
        animator.SetTrigger("castRod");
        canICast = false;

        // Wait for bait to be out of screen
        yield return new WaitForSeconds(1.4f);

        rbBait.isKinematic = true;
        baitAnimator.enabled = true;
        PlayerPrefs.SetInt("fishing", 1);

        yield return new WaitForSeconds(1);
        SoundManager.PlayFXSound(AudioFXSounds.splashFishing);

        // Wait for Animation to end
        yield return new WaitForSeconds(4);


        canIReel = true;

        counterOfCasting++;
    }

    IEnumerator ReelRod()
    {
        animator.SetTrigger("reelRod");
        canIReel = false;
        clickCounter = 0;
        SoundManager.PlayFXSound(AudioFXSounds.reelFishing);
        yield return new WaitForSeconds(8);

        rbBait.isKinematic = false;
        PlayerPrefs.SetInt("fishing", 0);

        yield return new WaitForSeconds(1);
        baitAnimator.enabled = false;

        if (counterOfCasting == 1)
        {
            if (PlayerPrefs.GetInt("Gender") == 0)
            {
                SoundManager.PlayDialogue(AudioDialogue.Fail1Male);
            }
            else
            {
                SoundManager.PlayDialogue(AudioDialogue.Fail1Female);
            }
        }

        if (counterOfCasting == 2)
        {
            if (PlayerPrefs.GetInt("Gender") == 0)
            {
                SoundManager.PlayDialogue(AudioDialogue.Fail2Male);
            }
            else
            {
                SoundManager.PlayDialogue(AudioDialogue.Fail2Female);
            }
        }
        // Wait for Animation to end
        yield return new WaitForSeconds(2);

        if (clickCounter > 5 && counterOfCasting >= 3)
        {
            // Make Fish Appear and avoid from further playing
            Debug.Log("Got Fish!");
            gotFish = true;

            SoundManager.PlayFXSound(AudioFXSounds.CompleteJob);

            canvasShowFishAnimator.SetTrigger("showFish");

            yield return new WaitForSeconds(2);

            if (PlayerPrefs.GetInt("Gender") == 0)
            {
                SoundManager.PlayDialogue(AudioDialogue.Win1Male);
            }
            else
            {
                SoundManager.PlayDialogue(AudioDialogue.Win1Female);
            }

            yield return new WaitForSeconds(5);

            canvasAnimator.SetTrigger("fadeIn");

            Debug.Log("Roll Credits after this!");

            yield return new WaitForSeconds(5);

            SceneManager.LoadScene(7);

        }

        if (!gotFish)
        {
            canICast = true;
            Debug.Log("Keep Trying!");
        }
    }

}
