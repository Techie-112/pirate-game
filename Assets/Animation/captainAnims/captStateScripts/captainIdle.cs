using UnityEngine;

public class captainIdle : StateMachineBehaviour
{
    //other variables
    captain captain;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        captain = animator.GetComponent<captain>();
    }
}
