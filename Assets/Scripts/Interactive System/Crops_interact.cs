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
        Disappear_Key();
    }
}
