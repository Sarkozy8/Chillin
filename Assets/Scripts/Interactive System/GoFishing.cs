using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoFishing : InteractBase
{
    public override void Appear_Key()
    {
        Debug.Log("Interact Key Appear");
        textForInteracts.text = "Go Fishing!";
        canvasAnimator.SetBool("showKey", true);
    }

    public override void Disappear_Key()
    {
        Debug.Log("Interact Key Dissapear");
        canvasAnimator.SetBool("showKey", false);
    }

    public override void Interact()
    {
        StartCoroutine(LoadFishing());
    }

    IEnumerator LoadFishing()
    {
        canvasAnimator.SetTrigger("fadeIn");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(5);
    }
}
