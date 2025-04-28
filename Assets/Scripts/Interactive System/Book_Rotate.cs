using UnityEngine;

public class Book_Rotate : InteractBase
{
    public override void Appear_Key()
    {
        Debug.Log("Book Key Appear");
    }

    public override void Disappear_Key()
    {
        Debug.Log("Book Key Dissapear");
    }

    public override void Interact()
    {
        Debug.Log("Interact with Book");
    }

}
