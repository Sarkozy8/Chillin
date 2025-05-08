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
        Disappear_Key();
    }
}
