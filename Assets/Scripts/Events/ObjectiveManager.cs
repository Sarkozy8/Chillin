using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    /// <summary>
    /// I would have made an objective manager for each scene, lowering the complexity, but since the same thing repeats many times, I just added toggle and if statements to avoid rebundance
    /// </summary>

    public Animator objectiveAnimator;
    public TextMeshProUGUI objectiveText;
    public ParticleSystem toothbrushPS;
    public ParticleSystem outdoorObjectivePS;

    public Animator canvasInteractAnimator; // This is the animator used for the fadeIn

    private bool toggleForBrushing = false;
    private bool toggleForHarvesting = false;


    void Start()
    {
        // Reset Brushing Objective for each day
        PlayerPrefs.SetInt("didIBrushToday", 0);
        StartCoroutine(DelayForBrushingTeethAfterWakingUp());

        // Fade out for Winter because of the transition
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            canvasInteractAnimator.SetTrigger("winterFadeOut");
        }

        // Reset Crops count for spring
        PlayerPrefs.SetInt("cropsHarvested", 0);
        PlayerPrefs.SetInt("enoughChilling", 0);
    }

    void Update()
    {

        // Detects if you brushed today
        if (PlayerPrefs.GetInt("didIBrushToday") == 1 && !toggleForBrushing)
        {
            objectiveAnimator.SetBool("showObjective", false);
            toggleForBrushing = true;

            // Disable Particle star system
            var emission = toothbrushPS.emission;
            emission.rateOverTime = 0;

            StartCoroutine(NextObjectiveAfterBrushing());
        }

        // Detects if you harvest more than 4 crops
        if (PlayerPrefs.GetInt("cropsHarvested") > 4 && !toggleForHarvesting)
        {
            objectiveAnimator.SetBool("showObjective", false);
            toggleForHarvesting = true;

            // Display Completed Objective Stars
            PlayerPrefs.SetInt("objectiveCompleted", 1);

            StartCoroutine(ChillForSpring());
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

    IEnumerator NextObjectiveAfterBrushing()
    {

        // For All seasons, we chill after brushing teeth, for spring we go straight to harvesting crops and the we chill and fadeout
        if (SceneManager.GetActiveScene().buildIndex != 3 && SceneManager.GetActiveScene().buildIndex != 4)
        {
            yield return new WaitForSeconds(25);
            objectiveAnimator.SetBool("showObjective", true);
            objectiveText.text = "Chill";
        }
        // For Spring
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            yield return new WaitForSeconds(4);
            objectiveAnimator.SetBool("showObjective", true);
            objectiveText.text = "Harvest Crops!";
        }
        // For summer, we skip chilling and go fishing
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            // Gotta wait 25 seconds since we brush in summer
            yield return new WaitForSeconds(25);
            objectiveAnimator.SetBool("showObjective", true);
            objectiveText.text = "Go Fishing!";
            var emission = outdoorObjectivePS.emission;
            emission.rateOverTime = 2;
        }

        // For Winter, after 20 seconds, go animal sighting
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            yield return new WaitForSeconds(60);
            objectiveAnimator.SetBool("showObjective", false);
            yield return new WaitForSeconds(3);
            objectiveText.text = "Go Animal Sighting";
            objectiveAnimator.SetBool("showObjective", true);
            PlayerPrefs.SetInt("enoughChilling", 1);
            var emission = outdoorObjectivePS.emission;
            emission.rateOverTime = 2;
        }
    }

    IEnumerator ChillForSpring()
    {
        yield return new WaitForSeconds(3);
        objectiveText.text = "Chill";
        objectiveAnimator.SetBool("showObjective", true);

        // Bruh, just chill for a minite and a half and then we keep moving. Take a breather
        yield return new WaitForSeconds(90);

        canvasInteractAnimator.SetTrigger("fadeIn");

        yield return new WaitForSeconds(4);

        SceneManager.LoadScene(4);
    }

}

