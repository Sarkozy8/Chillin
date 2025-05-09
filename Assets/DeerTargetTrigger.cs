using UnityEngine;

public class DeerTargetTrigger : MonoBehaviour
{
    private bool hasBeenTriggered = false;

    public void OnLookedAt()
    {
        if (!hasBeenTriggered)
        {
            hasBeenTriggered = true;
            Debug.Log($"{gameObject.name} was looked at with correct zoom!");
            // Insert your event logic here (e.g., show UI, play sound, unlock path, etc.)
        }
    }
}
