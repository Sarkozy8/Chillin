using UnityEngine;

public class Crops_interact : InteractBase
{
    public override void Appear_Key()
    {
        Debug.Log("Interact Key Appear");
        textForInteracts.text = "Crops";
        canvasAnimator.SetBool("showKey", true);
    }

    public override void Disappear_Key()
    {
        Debug.Log("Interact Key Dissapear");
        canvasAnimator.SetBool("showKey", false);
    }

    public override void Interact()
    {
        Debug.Log("Interact with Crops");

        if (PlayerPrefs.GetInt("Gender") == 0)
        {
            // Male dialogue
            if (PlayerPrefs.GetInt("CropsInteract") == 1)
                SoundManager.PlayDialogue(AudioDialogue.Crops1Male);

            if (PlayerPrefs.GetInt("CropsInteract") == 2)
                SoundManager.PlayDialogue(AudioDialogue.Crops2Male);

            if (PlayerPrefs.GetInt("CropsInteract") == 3)
                SoundManager.PlayDialogue(AudioDialogue.Crops3Male);

            PlayerPrefs.SetInt("CropsInteract", PlayerPrefs.GetInt("CropsInteract") + 1);
        }
        else
        {
            if (PlayerPrefs.GetInt("CropsInteract") == 1)
                SoundManager.PlayDialogue(AudioDialogue.Crops1Female);

            if (PlayerPrefs.GetInt("CropsInteract") == 2)
                SoundManager.PlayDialogue(AudioDialogue.Crops2Female);

            if (PlayerPrefs.GetInt("CropsInteract") == 3)
                SoundManager.PlayDialogue(AudioDialogue.Crops3Female);

            PlayerPrefs.SetInt("CropsInteract", PlayerPrefs.GetInt("CropsInteract") + 1);
        }

        Disappear_Key();
    }
}
