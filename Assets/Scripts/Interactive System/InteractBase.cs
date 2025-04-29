using UnityEngine;

//  Note: This is a "Base Class" where values needed between interact are kept.
//  Remember that these values get reset by scenes and not saved through them.
//  The interface "IInteractable" is used to make the "template" for the interacts. 

public abstract class InteractBase : MonoBehaviour, IInteractable
{

    public bool canInteract = true;

    public abstract void Appear_Key();

    public abstract void Disappear_Key();

    public abstract void Interact();

}
