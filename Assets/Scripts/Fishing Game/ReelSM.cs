using UnityEngine;

public class ReelSM : StateMachineBehaviour
{
    /// <summary>
    /// After casting, it will wait a bit before a fish starts moving the string and when trigger, it will activate reeling.
    /// </summary>

    private bool toggle = false;
    private float timerForCatch = 0;
    private float timer = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        toggle = false;
        timerForCatch = UnityEngine.Random.Range(30f, 90f);
        timer = 0;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if (timer > timerForCatch && animator.GetBool("catch") == false)
        {
            animator.SetBool("catch", true);
        }

        if (PlayerPrefs.GetInt("fishing") == 0 && !toggle)
        {
            animator.SetTrigger("reel");
            toggle = true;
        }
    }
}
