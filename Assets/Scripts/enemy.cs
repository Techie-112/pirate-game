using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
    //the enemy's stats
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public int damage = 1;
    player player;
    //charge mechanic variables
    public float chargeForce = 500f;
    public float stopDistance = 3f;
    public float pauseTime = 1f;
    public float chargeDrag = 8f; //drag doesn't work right now I'll fix it LATER!!!
    public float chargeDuration;
    private Vector2 chargeDirection;
    private enum EnemyState { Approaching, Pausing, Charging }
    private EnemyState currentState = EnemyState.Approaching;

    //variables related to enemy movement
    private Transform target;
    private Vector2 direction;
    private float angle;
    private Rigidbody2D rb;

    //variables related to enemy rotation
    private SpriteRenderer cursprite;
    [SerializeField] Sprite[] sprites;
    //0 = back 1 = right 2 = front 3 = left
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        cursprite = GetComponent<SpriteRenderer>();
        rb.linearDamping = 0;  // Ensure normal movement has no drag
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        if (currentState == EnemyState.Approaching)
        {
            ApproachPlayer();
        }
        else if (currentState == EnemyState.Pausing)
        {
            //this is just here for funsies I guess
        }
        else if ( currentState == EnemyState.Charging)
        {
            //Charge();
        }
        else
        {
            Debug.Log("currentState is somehow none of the above. It is: " +  currentState);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Wall")
        {
            if (collision.gameObject.tag == "Player")
            {
                player playerScript = collision.gameObject.GetComponent<player>();
                playerScript.TakeDamage();
            }
            else
            {
            Destroy(collision.gameObject);
            }
        }



    }


    private void ApproachPlayer()
    {
        /*this.angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //get angle between current rotation and target position
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //rotate towards the target
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed); */

        //get distance from player to this enemy
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            //move towards the player
            direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            //decide which way this enemy is facing
            if (rb.linearVelocityX > rb.linearVelocityY)
            {
                if (rb.linearVelocityX >= 0)
                {
                    //going right
                    cursprite.sprite = sprites[1];
                }
                else
                {
                    //going left
                    cursprite.sprite = sprites[3];
                }
            }
            else
            {
                if (rb.linearVelocityY >= 0)
                {
                    //going up
                    cursprite.sprite = sprites[0];
                }
                else
                {
                    //going down
                    cursprite.sprite = sprites[2];
                }
            }

        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(PauseBeforeCharge());
        }
    }
    IEnumerator PauseBeforeCharge()
    {
        currentState = EnemyState.Pausing;
        chargeDirection = (target.position - transform.position).normalized;
        //gets direction BEFORE the pause, letting the player dodge
        yield return new WaitForSeconds(pauseTime);

        currentState = EnemyState.Charging;
        rb.linearDamping = chargeDrag;  // Apply drag to slow down gradually
        Debug.Log("chargeDirection: " + chargeDirection + "] wah [" + chargeForce);
        rb.linearVelocity = chargeDirection * chargeForce;

        yield return new WaitForSeconds(chargeDuration);  // Give time to charge
        rb.linearDamping = 0;  // Reset drag for normal movement
        currentState = EnemyState.Approaching;

        //drag doesn't work right now I'll fix it LATER!!!
    }
}
