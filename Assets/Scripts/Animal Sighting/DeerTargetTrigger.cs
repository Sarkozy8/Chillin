using System;
using UnityEngine;

public class DeerTargetTrigger : MonoBehaviour
{
    /// <summary>
    /// This scripts is inside of every deer and when looked at, it will show you that you did succesfully looked at a deer. Great job Bob!
    /// </summary>


    private bool hasBeenTriggered = false;

    public void OnLookedAt()
    {
        if (!hasBeenTriggered)
        {
            hasBeenTriggered = true;
            Debug.Log($"{gameObject.name} was looked at with correct zoom!");

            StartCoroutine(SightingManager.TriggerDeer());
        }
    }

}
