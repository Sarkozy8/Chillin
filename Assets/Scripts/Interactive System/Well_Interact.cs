using UnityEngine;

public class Well_Interact : InteractBase
{
    public override void Appear_Key()
    {
        Debug.Log("Interact Key Appear");
        textForInteracts.text = "Water Well";
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
            if (PlayerPrefs.GetInt("WellInteract") == 1)
                SoundManager.PlayDialogue(AudioDialogue.Well1Male);

            if (PlayerPrefs.GetInt("WellInteract") == 2)
                SoundManager.PlayDialogue(AudioDialogue.Well2Male);

            PlayerPrefs.SetInt("WellInteract", PlayerPrefs.GetInt("WellInteract") + 1);
        }
        else
        {
            if (PlayerPrefs.GetInt("WellInteract") == 1)
                SoundManager.PlayDialogue(AudioDialogue.Well1Female);

            if (PlayerPrefs.GetInt("WellInteract") == 2)
                SoundManager.PlayDialogue(AudioDialogue.Well2Female);

            PlayerPrefs.SetInt("WellInteract", PlayerPrefs.GetInt("WellInteract") + 1);
        }

        Disappear_Key();
    }

}
