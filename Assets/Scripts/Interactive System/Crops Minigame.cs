using UnityEngine;
using UnityEngine.UI;

public class CropsMinigame : InteractBase
{
    private bool alreadyHarvested = false;
    private bool minigameEnded = false;

    // Game Values
    private float offset = 0.22f;
    private float interval = 1.0f;
    private float tolerance = 0.13f;
    private float maxTime = 10f;
    private float startTime;
    private bool gameOver = false;

    // UI needed values
    private Graphic photoRedLine;
    private RectTransform rotationRedLine;
    private Graphic photoYellowCircle;

    private void Start()
    {
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

    }

    private void Update()
    {
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
                }
                else
                {
                    Debug.Log("Bad timing! You lose.");
                    DisappearUI();
                    player.cameraCanMove = true;
                    player.playerCanMove = true;
                    player.enableHeadBob = true;
                    gameOver = true;
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
    }

    void DisappearUI()
    {
        Color colorCircle = photoYellowCircle.color;
        colorCircle.a = 0;
        photoYellowCircle.color = colorCircle;

        Color colorLine = photoRedLine.color;
        colorLine.a = 0;
        photoRedLine.color = colorLine;
    }

    public override void Appear_Key()
    {
        if (!alreadyHarvested)
        {
            Debug.Log("Interact Key Appear");
            textForInteracts.text = "Harvest Crop!";
            canvasAnimator.SetBool("showKey", true);
        }
    }

    public override void Disappear_Key()
    {
        if (!alreadyHarvested)
        {
            canvasAnimator.SetBool("showKey", false);
        }
    }

    public override void Interact()
    {
        if (!alreadyHarvested)
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
