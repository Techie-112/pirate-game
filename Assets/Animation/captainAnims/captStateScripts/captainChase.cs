using UnityEngine;

public class captainChase : StateMachineBehaviour
{

    //captain's stats
    [SerializeField] float speed = 4.5f;
    float biteRange = 1f;

    //other variables
    Transform player;
    Rigidbody2D rb2d;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //get the player's location
        Vector2 target = new Vector2(player.position.x, player.position.y);

        //shimmy the captain towards the player
        //param 1 takes current position, param 2 takes player position, param 3 is movement speed
        rb2d.MovePosition(Vector2.MoveTowards(rb2d.position, target, speed * Time.fixedDeltaTime));

        checkDistance(animator);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("bite");
        animator.SetInteger("curState", 1);
    }

    void checkDistance(Animator animator)
    {
        if(Vector2.Distance(player.position, rb2d.position) <= biteRange)
        {
            animator.SetTrigger("bite");
        }
    }
}
