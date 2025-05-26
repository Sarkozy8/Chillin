using UnityEngine;

public class WakeUpSM : StateMachineBehaviour
{

    /// <summary>
    /// After wake up animation is done, let the player start moving.
    /// </summary>


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("wakeUp", false);
    }

}
