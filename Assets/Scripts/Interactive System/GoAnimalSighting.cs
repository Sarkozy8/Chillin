using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoAnimalSighting : InteractBase
{
    public override void Appear_Key()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("Interact Key Appear");
            textForInteracts.text = "Go Animal Sighting!";
            canvasAnimator.SetBool("showKey", true);
        }
    }


    public override void Disappear_Key()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("Interact Key Dissapear");
            canvasAnimator.SetBool("showKey", false);
        }
    }

    public override void Interact()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(LoadAnimalSighting());
        }
    }

    IEnumerator LoadAnimalSighting()
    {
        canvasAnimator.SetTrigger("fadeIn");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }
}
