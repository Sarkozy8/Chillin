using UnityEngine;

public class ThrowSMM : StateMachineBehaviour
{
    /// <summary>
    /// Prevents logic from breaking on fishing minigame.
    /// </summary>

    private bool toggle = false;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        toggle = false;
        animator.SetBool("catch", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerPrefs.GetInt("fishing") == 1 && !toggle)
        {
            animator.SetTrigger("throw");
            toggle = true;
        }
    }
}
