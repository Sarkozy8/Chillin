using UnityEngine;

public class Pond_interact : InteractBase
{
    public override void Appear_Key()
    {
        Debug.Log("Interact Key Appear");
        textForInteracts.text = "Little Pond";
        canvasAnimator.SetBool("showKey", true);
    }

    public override void Disappear_Key()
    {
        Debug.Log("Interact Key Dissapear");
        canvasAnimator.SetBool("showKey", false);
    }

    public override void Interact()
    {
        Debug.Log("Interact with Well");

        if (PlayerPrefs.GetInt("Gender") == 0)
        {
            // Male dialogue
            if (PlayerPrefs.GetInt("PondInteract") == 1)
                SoundManager.PlayDialogue(AudioDialogue.Pond1Male);

            if (PlayerPrefs.GetInt("PondInteract") == 2)
                SoundManager.PlayDialogue(AudioDialogue.Pond2Male);

            if (PlayerPrefs.GetInt("PondInteract") == 3)
                SoundManager.PlayDialogue(AudioDialogue.Pond3Male);

            PlayerPrefs.SetInt("PondInteract", PlayerPrefs.GetInt("PondInteract") + 1);
        }
        else
        {
            if (PlayerPrefs.GetInt("PondInteract") == 1)
                SoundManager.PlayDialogue(AudioDialogue.Pond1Female);

            if (PlayerPrefs.GetInt("PondInteract") == 2)
                SoundManager.PlayDialogue(AudioDialogue.Pond2Female);

            if (PlayerPrefs.GetInt("PondInteract") == 3)
                SoundManager.PlayDialogue(AudioDialogue.Pond3Female);

            PlayerPrefs.SetInt("PondInteract", PlayerPrefs.GetInt("PondInteract") + 1);
        }

        Disappear_Key();
    }
}
