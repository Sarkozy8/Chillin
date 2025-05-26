using UnityEngine;

public class DialogueEventsFunctions : MonoBehaviour
{
    /// <summary>
    /// This script is used to play dialogue on a specific place of an animation.
    /// </summary>

    public void PLayDialogueIntoFishing()
    {
        if (PlayerPrefs.GetInt("Gender") == 0)
        {
            SoundManager.PlayDialogue(AudioDialogue.ChillingMale);
        }
        else
        {
            SoundManager.PlayDialogue(AudioDialogue.ChillingFemale);
        }
    }

    public void PLayDialogueIntoAnimalSighting()
    {
        if (PlayerPrefs.GetInt("Gender") == 0)
        {
            SoundManager.PlayDialogue(AudioDialogue.deerMale);
        }
        else
        {
            SoundManager.PlayDialogue(AudioDialogue.deerFemale);
        }
    }

}
