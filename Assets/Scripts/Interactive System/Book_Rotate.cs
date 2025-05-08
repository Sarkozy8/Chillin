using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Book_Rotate : InteractBase
{
    public Transform inspectTransform;                                         // Transform used for parenting the instantiated object
    public GameObject bookObject;                                              // Object to rotate (make sure it has no colliders)
    private GameObject instanceOfObject;                                       // Actual instance of the object when inspecting
    private Quaternion baseRotationObject = Quaternion.Euler(170f, 180f, 90f); // Base rotation for books
    public float rotationSpeed = 5f;                                           // Rotation Speed for Inspect
    private Vector3 currentRotation;                                           // Gets current rotation of object on X, and Y axis for later clamping
    private Vector3 previousMousePosition;                                     // Previous mouse position for pivoting the rotation

    private bool areWeInteracting = false;                                     // Toggle for Interacting

    private PostProcessVolume v;
    private DepthOfField dof;                                                  // Apparently you need a deft of field object top modify it on the volume


    private void Start()
    {
        v = GameObject.Find("PP Volume").GetComponent<PostProcessVolume>();

        if (v == null)
        {
            Debug.LogError("No PostProcessVolume found!");
            return;
        }

        // Get the Depth of Field settings
        v.profile.TryGetSettings(out dof);

        if (dof == null)
        {
            Debug.LogError("Depth of Field effect not found in PostProcessProfile!");
            return;
        }

        // Start with Depth of Field disabled
        dof.enabled.Override(false);

    }

    private void Update()
    {

        if (!areWeInteracting && !canInteract) // Used to avoid duplicating the items and being stuck in interact
                                               // It is position here because of the hierarchy on the interact function
        {
            canInteract = true;
        }

        if (areWeInteracting)
        {

            if (Input.GetMouseButtonDown(0))    // Get last mouse position (pivot point)
            {
                previousMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))    // Logic for rotation
            {
                Vector3 delta = Input.mousePosition - previousMousePosition;

                currentRotation.x += delta.y * rotationSpeed * Time.deltaTime;
                currentRotation.y += -delta.x * rotationSpeed * Time.deltaTime;

                currentRotation.x = Mathf.Clamp(currentRotation.x, -90f, 90f);

                instanceOfObject.transform.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, -90f);

                previousMousePosition = Input.mousePosition;
            }

            if (Input.GetKeyDown(KeyCode.E))    // Deinteract object logic
            {
                Destroy(instanceOfObject);
                player.cameraCanMove = true;
                player.playerCanMove = true;
                player.enableHeadBob = true;
                areWeInteracting = false;
                Cursor.lockState = CursorLockMode.Locked;

                dof.enabled.Override(false);
                Disappear_Key();
            }
        }
    }

    public override void Appear_Key()
    {
        Debug.Log("Interact Key Appear");
        textForInteracts.text = "Inspect Book";
        canvasAnimator.SetBool("showKey", true);
    }

    public override void Disappear_Key()
    {
        Debug.Log("Interact Key Dissapear");
        canvasAnimator.SetBool("showKey", false);
    }

    public override void Interact()
    {
        if (areWeInteracting == false && canInteract == true)
        {
            Debug.Log("Interact with Book");

            instanceOfObject = Instantiate(bookObject, new Vector3(0, 0, 0), baseRotationObject, inspectTransform); // Make item appear
            instanceOfObject.transform.localPosition = new Vector3(0, 0, 0);    //Position the item properly in front of player

            areWeInteracting = true;         //Enables interacting logic
            Cursor.lockState = CursorLockMode.Confined;
            canInteract = false;             // Avoid further interacting until this interaction is done

            player.cameraCanMove = false;    // Stop player from moving
            player.playerCanMove = false;
            player.enableHeadBob = false;

            dof.enabled.Override(true);

            textForInteracts.text = "Exit";
        }
    }

}
