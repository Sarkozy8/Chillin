using UnityEngine;

public class CompletedTask : MonoBehaviour
{
    /// <summary>
    /// A simple script that activates the little starts after completing an objective.
    /// </summary>

    private Animator canvasAnimator;

    void Start()
    {
        canvasAnimator = GetComponent<Animator>();
        PlayerPrefs.SetInt("objectiveCompleted", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("objectiveCompleted") == 1)
        {
            PlayerPrefs.SetInt("objectiveCompleted", 0);
            canvasAnimator.SetTrigger("objectiveCompleted");
        }
    }
}
