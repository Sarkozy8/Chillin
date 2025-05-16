using System.Collections;
using UnityEngine;

public class FishingMovementAndCasting : MonoBehaviour
{
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

        // Wait for Animation to end
        yield return new WaitForSeconds(5);

        canIReel = true;
    }

    IEnumerator ReelRod()
    {
        animator.SetTrigger("reelRod");
        canIReel = false;
        clickCounter = 0;

        // Wait for Animation to end
        yield return new WaitForSeconds(11);

        if (clickCounter > 5)
        {
            // Make Fish Appear and avoid from further playing
            Debug.Log("Got Fish!");
            gotFish = true;

        }

        if (!gotFish)
        {
            canICast = true;
            Debug.Log("Keep Trying!");
        }
    }

}
