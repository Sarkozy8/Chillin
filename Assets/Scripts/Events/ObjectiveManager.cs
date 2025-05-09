using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{

    public Animator objectiveAnimator;
    public TextMeshProUGUI objectiveText;
    public ParticleSystem toothbrushPS;

    public Animator canvasInteractAnimator; // This is the animator used for the fadeIn

    private bool toggleForBrushing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("didIBrushToday", 0);
        StartCoroutine(DelayForBrushingTeethAfterWakingUp());

        // Fade out for Winter because of the transition
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            canvasInteractAnimator.SetTrigger("winterFadeOut");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("didIBrushToday") == 1 && !toggleForBrushing)
        {
            objectiveAnimator.SetBool("showObjective", false);
            toggleForBrushing = true;

            // Disable Particle star system
            var emission = toothbrushPS.emission;
            emission.rateOverTime = 0;

            StartCoroutine(DelayForChilling());
        }
    }

    IEnumerator DelayForBrushingTeethAfterWakingUp()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            // For winter
            yield return new WaitForSeconds(2);
        }
        else
        {
            // For the rest of seasons
            yield return new WaitForSeconds(15);
        }

        objectiveAnimator.SetBool("showObjective", true);
        objectiveText.text = "Brush your Teeth!";
    }

    IEnumerator DelayForChilling()
    {
        yield return new WaitForSeconds(25);
        objectiveAnimator.SetBool("showObjective", true);
        objectiveText.text = "Chill";

        // For Winter, after 20 seconds, go 
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            yield return new WaitForSeconds(25);
            objectiveAnimator.SetBool("showObjective", false);
            yield return new WaitForSeconds(2);
            objectiveAnimator.SetBool("showObjective", true);
            objectiveText.text = "Go Animal Sighting";
        }
    }

}

