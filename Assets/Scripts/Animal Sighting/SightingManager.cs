using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SightingManager : MonoBehaviour
{
    /// <summary>
    /// As the name said, this is the sighting manager and will manage all the logic to make the minigame work (which deer to select, waiting to appear, when you found it)
    /// </summary>

    public static SightingManager Instance { get; private set; }  // Static reference to the instance

    public int randomDeer = 5;

    // Game objects for all deer locations
    public GameObject deer1;
    public GameObject deer2;
    public GameObject deer3;
    public GameObject deer4;
    public GameObject deer5;

    public Animator minigameAnimator;

    // Ensures only one instance of SightingManager exists in the scene
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;  // Set the static reference to this instance
        }
        else
        {
            Destroy(gameObject);  // Destroy if there is already an instance
        }
    }

    void Start()
    {

        // Selects Random deer
        randomDeer = UnityEngine.Random.Range(1, 6);
        StartCoroutine(DeerSelector());
    }

    IEnumerator DeerSelector()
    {
        // Wait before adding the deer to the scene
        yield return new WaitForSeconds(60);

        // Show the deer now
        switch (randomDeer)
        {
            case 1:
                deer1.SetActive(true);
                break;
            case 2:
                deer2.SetActive(true);
                break;
            case 3:
                deer3.SetActive(true);
                break;
            case 4:
                deer4.SetActive(true);
                break;
            case 5:
                deer5.SetActive(true);
                break;
        }
    }

    public static IEnumerator TriggerDeer()
    {
        // When deer is found, show that you did found it and go to the next season

        Instance.minigameAnimator.SetTrigger("foundTheDeer");

        SoundManager.PlayFXSound(AudioFXSounds.CompleteJob);
        yield return new WaitForSeconds(7);

        Instance.minigameAnimator.SetTrigger("fadeOut");

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(3);
    }

}
