using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;
public class Toothbrush_interact : InteractBase
{
    public Transform inspectTransform;         // Transform used for parenting the instantiated object
    public GameObject toothbrushObject;        // Object to rotate (make sure it has no colliders)
    private GameObject instanceOfObject;       // Actual instance of the object when inspecting
    private bool areWeInteracting = false;

    public float brushDuration = 15f;          // Seconds to make the minigame last
    private float movementScore = 0f;          // Score for minigame
    private float timer = 0f;                  // Retains how long has it past
    private bool brushing = false;             // Self explanatory
    public float moveSpeed = 0.03f;            // Move Speed for toothbrush (0.03 is the best)
    private Vector3 lastMousePos;              // Last position of mouse used to calculate movement each frame+

    private bool countDownDone = false;        // Toggle to start the brushing minigame


    void Update()
    {
        // Ingenius way to do what I was doing before with "areWeInteracting" but better (it also works nicely since it is time-base but you could just add an input and return to avoid further updates loading)

        if (!countDownDone) return;

        timer += Time.deltaTime;
        if (timer >= brushDuration)
        {
            // When timer reaches 0, stops brushing and give score
            brushing = false;
            countDownDone = false;
            EndBrushing();

            return;
        }

        // Get current mouse position
        Vector3 currentLocalPos = instanceOfObject.transform.localPosition;

        // Get last mouse position (pivot point)
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }

        // While clicking or "holding" the toothbrush, move inside of contraints and simulate brushing
        if (Input.GetMouseButton(0))
        {

            // Get mouse position compare to last (are we moving up.down,left,or right?)
            Vector3 delta = Input.mousePosition - lastMousePos;

            // Sanitaze X and Y values
            float deltaX = delta.x * moveSpeed * Time.deltaTime;
            float deltaY = delta.y * moveSpeed * Time.deltaTime;

            // Accumulate movement into toothbrush position
            currentLocalPos.x += deltaX;
            currentLocalPos.y += deltaY;

            // Clamp toothbrush position
            currentLocalPos.x = Mathf.Clamp(currentLocalPos.x, -0.16f, 0.16f);
            currentLocalPos.y = Mathf.Clamp(currentLocalPos.y, -0.04f, 0f);

            // Move toothbrush
            instanceOfObject.transform.localPosition = currentLocalPos;

            // Update position for next frame
            lastMousePos = Input.mousePosition;

            // Get a score to later display
            movementScore += delta.magnitude * moveSpeed;

            // Get particle system to show "bubbles" when moving
            ParticleSystem ps = instanceOfObject.GetComponentInChildren<ParticleSystem>();
            var emission = ps.emission;

            // Show bubbles if enough movement, else not show
            if (delta.magnitude * moveSpeed > 0.5f)
            {
                emission.enabled = true;
                emission.rateOverTime = 20f;
            }
            else
            {
                emission.rateOverTime = 0f;
            }

        }

    }



    public override void Interact()
    {

        if (areWeInteracting == false && canInteract == true && PlayerPrefs.GetInt("didIBrushToday") == 0)
        {
            Debug.Log("Interact with toothbrush");
            areWeInteracting = true;         //Enables interacting logic

            // Avoid further interacting until this interaction is done and player from moving
            canInteract = false;
            player.cameraCanMove = false;
            player.playerCanMove = false;
            player.enableHeadBob = false;

            // Used to the Interact keys doesnt appear when doing the minigame
            Disappear_Key();
            StartBrushing();
            canvasAnimator.SetTrigger("countDown");
            StartCoroutine(CountDown());

        }
    }

    public void StartBrushing()
    {
        brushing = true;
        timer = 0f;

        // Make toothbrush appear, 
        instanceOfObject = Instantiate(toothbrushObject, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), inspectTransform);
        inspectTransform.transform.localRotation = Quaternion.Euler(0, 0, 0);
        instanceOfObject.transform.localPosition = new Vector3(0.16f, -0.04f, -0.06f);    //Position the item properly in front of player
        instanceOfObject.transform.localRotation = Quaternion.Euler(0, 180f, -90f);

        // Used to not letting you brush your teeth again today
        PlayerPrefs.SetInt("didIBrushToday", 1);

        Cursor.lockState = CursorLockMode.Confined;
    }

    void EndBrushing()
    {

        Debug.Log("Brushing Done! Score: " + movementScore);

        movementScore = 0;

        // Clean stuff to keep playing
        Destroy(instanceOfObject);
        canInteract = true;
        areWeInteracting = false;
        player.cameraCanMove = true;
        player.playerCanMove = true;
        player.enableHeadBob = true;

        // Apparently the cursor is needed to track mouse movement so I had to enabled it before
        Cursor.lockState = CursorLockMode.Locked;

    }

    public override void Appear_Key()
    {
        if (PlayerPrefs.GetInt("didIBrushToday") == 0)
        {
            Debug.Log("Interact Key Appear");
            textForInteracts.text = "Brush your teeth!";
            canvasAnimator.SetBool("showKey", true);
        }
    }

    public override void Disappear_Key()
    {
        if (PlayerPrefs.GetInt("didIBrushToday") == 0)
        {
            Debug.Log("Interact Key Dissapear");
            canvasAnimator.SetBool("showKey", false);
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3);
        countDownDone = true;
    }

}
