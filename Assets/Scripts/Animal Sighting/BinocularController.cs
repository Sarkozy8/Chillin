using UnityEngine;

/// <summary>
/// This controls the player camera to look around and zoom in to find the deers. It also shoots rays to try and find the deer.
/// </summary>
public class BinocularController : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public float rotationX = 0f;
    public float rotationY = 0f;

    private Camera cam;
    private float[] zoomLevels = { 60f, 40f, 20f, 5f };
    private int currentZoomIndex = 0;

    public float zoomSpeed = 5f; // Controls zoom transition speed
    private float targetFOV;

    // Target Finding Values
    private float maxDistance = 200f;
    public SightingManager sightingManager;
    private bool found5thDeer = false;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("This script must be attached to a Camera.");
            enabled = false;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        targetFOV = zoomLevels[currentZoomIndex];
        cam.fieldOfView = targetFOV;

        Vector3 angles = transform.eulerAngles;
        rotationY = angles.y;
        rotationX = angles.x;
    }

    void Update()
    {
        // Values used for checking if I looked at the target
        RaycastHit hit;
        Vector3 rayOrigin = cam.transform.position;
        Vector3 rayDirection = cam.transform.forward;


        // Zoom in (Right Click)
        if (Input.GetMouseButtonDown(1))
        {
            if (currentZoomIndex < zoomLevels.Length - 1)
            {
                currentZoomIndex++;
                targetFOV = zoomLevels[currentZoomIndex];
            }
        }

        // Zoom out (Left Click)
        if (Input.GetMouseButtonDown(0))
        {
            if (currentZoomIndex > 0)
            {
                currentZoomIndex--;
                targetFOV = zoomLevels[currentZoomIndex];
            }
        }

        // Smoothly interpolate to the target FOV
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);

        // Mouse movement to rotate camera
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;

        // Clamp the angles
        rotationX = Mathf.Clamp(rotationX, 0f, 30f);
        rotationY = Mathf.Clamp(rotationY, -5f, 60f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // Only trigger if ray hits something
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxDistance))
        {
            // Check tag or layer of target object
            if (hit.collider.CompareTag("DeerTarget"))
            {
                // Check if current FOV is within a certain range (e.g., must be zoomed in)
                if (cam.fieldOfView <= 10f)
                {
                    // Trigger some event
                    hit.collider.GetComponent<DeerTargetTrigger>()?.OnLookedAt();
                }
            }
        }

        Debug.DrawRay(cam.transform.position, cam.transform.forward * maxDistance, Color.red);

        // Logic for 5th Deer Find
        if (sightingManager.randomDeer == 5 && !found5thDeer)
        {
            if (transform.rotation.y >= 0.42f && cam.fieldOfView >= 55f)
            {
                Debug.Log("5th Deer Found!");
                found5thDeer = true;

                StartCoroutine(SightingManager.TriggerDeer());
            }
        }

    }
}



