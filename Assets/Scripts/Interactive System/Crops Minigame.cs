using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CropsMinigame : InteractBase
{
    private bool alreadyHarvested = false;

    // Game Values
    private GameObject wholeVegetable;
    private Transform leafVegetable; // I dont get a transform of wholevegatable since it is rebundant and I needed the object to find the leaf
    private float offset = 0.22f;
    private float interval = 1.0f;
    private float tolerance = 0.13f;
    private float maxTime = 10f;
    private float startTime;
    private bool gameOver = false;
    private float durationForResult = 2f;
    private bool toggleGame = true;

    // UI needed values
    private Graphic photoRedLine;
    private RectTransform rotationRedLine;
    private Graphic photoYellowCircle;
    private Graphic photoKey;



    private void Start()
    {

        // Get oobject for minigame UI (we dont use animators, just make them appear and disappear)
        GameObject redLine = GameObject.Find("RedLine");
        if (redLine != null)
        {
            rotationRedLine = redLine.GetComponent<RectTransform>();
            photoRedLine = redLine.GetComponent<Graphic>();
        }
        else
        {
            Debug.LogError("redLine Object not found!");
        }

        GameObject yellowCircle = GameObject.Find("OutCircle");
        if (yellowCircle != null)
        {
            photoYellowCircle = yellowCircle.GetComponent<Graphic>();
        }
        else
        {
            Debug.LogError("YellowCircle Object not found!");
        }

        GameObject keyE = GameObject.Find("KeyCrops");
        if (keyE != null)
        {
            photoKey = keyE.GetComponent<Graphic>();
        }
        else
        {
            Debug.LogError("KeyE Object not found!");
        }

        // Set Gameobjects for displaying results
        wholeVegetable = this.gameObject;
        Transform leaf = wholeVegetable.transform.Find("layer 2");

        if (leaf != null)
        {
            leafVegetable = leaf;
            Debug.Log("Found leaf: " + leafVegetable.name);
        }
        else
        {
            Debug.LogWarning("Leaf not found under Crop.");
        }

    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("cropsHarvested") > 4 && toggleGame)
        {
            toggleGame = false;
            //StartCoroutine(DissapearCrops());
        }


        if (alreadyHarvested && !gameOver)
        {

            float currentTime = Time.time - startTime;

            RotateLine();

            if (currentTime > maxTime)
            {
                Debug.Log("TimeOut! Too Slow!!!");
                gameOver = true;
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsCorrectTiming(currentTime))
                {
                    Debug.Log("Good timing!");
                    DisappearUI();
                    player.cameraCanMove = true;
                    player.playerCanMove = true;
                    player.enableHeadBob = true;
                    gameOver = true;
                    StartCoroutine(ShowGoodResult());
                }
                else
                {
                    Debug.Log("Bad timing! You lose.");
                    DisappearUI();
                    player.cameraCanMove = true;
                    player.playerCanMove = true;
                    player.enableHeadBob = true;
                    gameOver = true;
                    StartCoroutine(ShowBadResult());
                }

                PlayerPrefs.SetInt("cropsHarvested", PlayerPrefs.GetInt("cropsHarvested") + 1);
            }

        }
    }


    void RotateLine()
    {
        if (rotationRedLine == null) return;

        float degreesPerSecond = 360f;
        rotationRedLine.Rotate(0f, 0f, -degreesPerSecond * Time.deltaTime);
    }

    bool IsCorrectTiming(float time)
    {
        float timeSinceFirstTarget = time - offset;
        if (timeSinceFirstTarget < 0)
            return false;

        float nearestTarget = Mathf.Round(timeSinceFirstTarget / interval) * interval + offset;
        float difference = Mathf.Abs(time - nearestTarget);

        return difference <= tolerance;
    }

    void AppearUI()
    {
        Color colorCircle = photoYellowCircle.color;
        colorCircle.a = 1f;
        photoYellowCircle.color = colorCircle;

        Color colorLine = photoRedLine.color;
        colorLine.a = 1f;
        photoRedLine.color = colorLine;

        Color colorKey = photoKey.color;
        colorKey.a = 1f;
        photoKey.color = colorKey;
    }

    void DisappearUI()
    {
        Color colorCircle = photoYellowCircle.color;
        colorCircle.a = 0;
        photoYellowCircle.color = colorCircle;

        Color colorLine = photoRedLine.color;
        colorLine.a = 0;
        photoRedLine.color = colorLine;

        Color colorKey = photoKey.color;
        colorKey.a = 0;
        photoKey.color = colorKey;
    }

    IEnumerator ShowGoodResult()
    {
        // Play Good Job! Audio
        SoundManager.PlayFXSound(AudioFXSounds.GoodJob);

        Vector3 startPos = wholeVegetable.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 0.5f, 0);

        float elapsed = 0f;

        while (elapsed < durationForResult)
        {
            wholeVegetable.transform.position = Vector3.Lerp(startPos, endPos, elapsed / durationForResult);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos; // Ensure final position is accurate

        wholeVegetable.SetActive(false);
    }

    IEnumerator ShowBadResult()
    {
        // Play Bad Job! Audio
        SoundManager.PlayFXSound(AudioFXSounds.BadJob);

        Vector3 startPos = leafVegetable.position;
        Vector3 endPos = startPos + new Vector3(0, 0.5f, 0);

        float elapsed = 0f;

        while (elapsed < durationForResult)
        {
            leafVegetable.position = Vector3.Lerp(startPos, endPos, elapsed / durationForResult);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos; // Ensure final position is accurate

        wholeVegetable.SetActive(false);
    }

    IEnumerator DissapearCrops()
    {
        yield return new WaitForSeconds(2);
        wholeVegetable.SetActive(false);
    }

    public override void Appear_Key()
    {
        if (!alreadyHarvested && PlayerPrefs.GetInt("didIBrushToday") == 1)
        {
            Debug.Log("Interact Key Appear");
            textForInteracts.text = "Harvest Crop!";
            canvasAnimator.SetBool("showKey", true);
        }
    }

    public override void Disappear_Key()
    {
        if (!alreadyHarvested & PlayerPrefs.GetInt("didIBrushToday") == 1)
        {
            canvasAnimator.SetBool("showKey", false);
        }
    }

    public override void Interact()
    {
        if (!alreadyHarvested & PlayerPrefs.GetInt("didIBrushToday") == 1)
        {
            Disappear_Key();
            alreadyHarvested = true;
            startTime = Time.time;

            player.cameraCanMove = false;
            player.playerCanMove = false;
            player.enableHeadBob = false;

            AppearUI();

            // Reset Red line so it is syncho
            rotationRedLine.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

}
