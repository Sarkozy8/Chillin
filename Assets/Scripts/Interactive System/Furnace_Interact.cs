using UnityEngine;

public class Furnace_Interact : InteractBase
{
    public override void Appear_Key()
    {
        Debug.Log("Interact Key Appear");
        textForInteracts.text = "Inspect";
        canvasAnimator.SetBool("showKey", true);
    }

    public override void Disappear_Key()
    {
        Debug.Log("Interact Key Dissapear");
        canvasAnimator.SetBool("showKey", false);
    }

    public override void Interact()
    {
        Debug.Log("Interact with Furnace");

        if (PlayerPrefs.GetInt("Gender") == 0)
        {
            // Male dialogue
            if (PlayerPrefs.GetInt("FurnaceInteract") == 1)
                SoundManager.PlayDialogue(AudioDialogue.Furnace1Male);

            if (PlayerPrefs.GetInt("FurnaceInteract") == 2)
                SoundManager.PlayDialogue(AudioDialogue.Furnace2Male);

            if (PlayerPrefs.GetInt("FurnaceInteract") == 3)
                SoundManager.PlayDialogue(AudioDialogue.Furnace3Male);

            PlayerPrefs.SetInt("FurnaceInteract", PlayerPrefs.GetInt("FurnaceInteract") + 1);
        }
        else
        {
            if (PlayerPrefs.GetInt("FurnaceInteract") == 1)
                SoundManager.PlayDialogue(AudioDialogue.Furnace1Female);

            if (PlayerPrefs.GetInt("FurnaceInteract") == 2)
                SoundManager.PlayDialogue(AudioDialogue.Furnace2Female);

            if (PlayerPrefs.GetInt("FurnaceInteract") == 3)
                SoundManager.PlayDialogue(AudioDialogue.Furnace3Female);

            PlayerPrefs.SetInt("FurnaceInteract", PlayerPrefs.GetInt("FurnaceInteract") + 1);
        }

        Disappear_Key();
    }
}
