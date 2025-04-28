using UnityEngine;

public abstract class InteractBase : MonoBehaviour, IInteractable
{

    public bool showKey = true; // Used to avoid showing key to press
    public bool toothbrushHasBeenInteracted = false; //Remember if you brush your teeth today


    public abstract void Appear_Key();

    public abstract void Disappear_Key();

    public abstract void Interact();

}
