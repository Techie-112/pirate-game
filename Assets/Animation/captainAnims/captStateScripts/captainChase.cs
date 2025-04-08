using Unity.VisualScripting;
using UnityEngine;

public class captainChase : StateMachineBehaviour
{
    //other variables
    captain captain;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        captain = animator.GetComponent<captain>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        captain.move();

         if (captain.inRange == true)
        {
            animator.SetTrigger("bite");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("bite");
        animator.SetInteger("curState", 1);
    }


}
