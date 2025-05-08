using System.Collections;
using TMPro;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{

    public Animator objectiveAnimator;
    public TextMeshProUGUI objectiveText;
    public ParticleSystem toothbrushPS;

    private bool toggleForBrushing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("didIBrushToday", 0);
        StartCoroutine(DelayForBrushingTeethAfterWakingUp());

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
        yield return new WaitForSeconds(15);
        objectiveAnimator.SetBool("showObjective", true);
        objectiveText.text = "Brush your Teeth!";
    }

    IEnumerator DelayForChilling()
    {
        yield return new WaitForSeconds(25);
        objectiveAnimator.SetBool("showObjective", true);
        objectiveText.text = "Chill";
    }

}

