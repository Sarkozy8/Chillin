using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SightingManager : MonoBehaviour
{
    public static SightingManager Instance { get; private set; }  // Static reference to the instance

    public int randomDeer = 5;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomDeer = UnityEngine.Random.Range(1, 6);
        StartCoroutine(DeerSelector());
    }

    IEnumerator DeerSelector()
    {
        yield return new WaitForSeconds(2);

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

        Instance.minigameAnimator.SetTrigger("foundTheDeer");
        Debug.Log("Tried");
        yield return new WaitForSeconds(7);

        Instance.minigameAnimator.SetTrigger("fadeOut");

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(3);
    }

}
