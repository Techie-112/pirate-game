using UnityEngine;
using System.Collections;
using System;

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
    public float chargeDuration;
    public float slowdownRate = 0.99f; // How much speed decreases per frame
    private bool isOnCooldown = false;
    private Vector2 chargeDirection;
    public float cooldownTime = 1f; // Cooldown after charge
    private enum EnemyState { Approaching, Pausing, Charging, Cooldown }
    private EnemyState currentState = EnemyState.Approaching;
    public float chargeDrag = 1.5f; // Adjust for smoother slowdown

    //variables related to enemy movement
    private Transform target;
    private Vector2 direction;
    private float angle;
    private Rigidbody2D rb;

    //variables related to enemy rotation
    private SpriteRenderer cursprite;
    [SerializeField] Sprite[] sprites;
    //0 = back 1 = right 2 = front 3 = left

    //reference to wavespawner
    wavespawner ws;

    Vector2 Current_velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        cursprite = GetComponent<SpriteRenderer>();
        rb.linearDamping = 0;  // Ensure normal movement has no drag
        ws = GameObject.FindWithTag("ws").GetComponent<wavespawner>();
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
        else
        {
            //Debug.Log("currentState is: " +  currentState);
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

    private void OnDestroy()
    {
        ws.enemiesLeft--;
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

        if (distance > stopDistance || isOnCooldown)
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
    /*IEnumerator PauseBeforeCharge()
    {
        currentState = EnemyState.Pausing;
        rb.linearVelocity = Vector2.zero; // Stop movement
        chargeDirection = (target.position - transform.position).normalized; //get direction before pausing, allowing dodging
        yield return new WaitForSeconds(pauseTime);

        if (chargeDirection == Vector2.zero) chargeDirection = Vector2.right; // Prevent zero direction issues

        currentState = EnemyState.Charging;

        rb.linearVelocity = chargeDirection * chargeForce;
        //rb.AddForce(chargeDirection * chargeForce, ForceMode2D.Impulse); // Apply force

        StartCoroutine(SlowDown()); // Start slowdown process
        yield return new WaitForSeconds(chargeDuration);

        currentState = EnemyState.Approaching;
    }
    IEnumerator SlowDown()
    {
        float timer = 0;
        while (timer < chargeDuration)
        {
            rb.linearVelocity *= slowdownRate;
            timer += Time.deltaTime;
            yield return null;
        }

        currentState = EnemyState.Approaching;
        isOnCooldown = true; // Start cooldown
        //Debug.Log("Cooldown starts");
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false; // Reset cooldown
        //Debug.Log("Cooldown ends");
    } */
    IEnumerator PauseBeforeCharge()
    {
        currentState = EnemyState.Pausing;
        rb.linearVelocity = Vector2.zero; // Stop movement
        yield return new WaitForSeconds(pauseTime);

        chargeDirection = (target.position - transform.position).normalized;
        if (chargeDirection == Vector2.zero) chargeDirection = Vector2.right; // Prevent zero direction issues

        currentState = EnemyState.Charging;
        rb.linearDamping = chargeDrag; // Apply drag for gradual slowdown

        rb.linearVelocity = chargeDirection * chargeForce; // Give an initial velocity boost
        
        yield return new WaitForSeconds(chargeDuration); // Let the charge happen
        rb.linearDamping = 0; // Reset drag for normal movement

        currentState = EnemyState.Approaching;
        isOnCooldown = true; // Start cooldown
        //Debug.Log("Cooldown starts");
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false; // Reset cooldown
        //Debug.Log("Cooldown ends");

        currentState = EnemyState.Approaching;
    }

    IEnumerator SlowDown()
    {
        float timer = 0;
        while (timer < chargeDuration)
        {
            rb.linearVelocity *= slowdownRate; // Reduce speed gradually
            timer += Time.deltaTime;
            yield return null;
        }

        
    }

}
