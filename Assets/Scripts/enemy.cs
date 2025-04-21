using UnityEngine;
using System.Collections;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{
    //the enemy's stats
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public int damage = 1;

    //charge mechanic variables
    public float chargeForce = 500f;
    public float stopDistance = 3f;
    public float pauseTime = 1f;
    public float chargeDuration;
    private bool isOnCooldown = false;
    private Vector2 chargeDirection;
    public float cooldownTime = 1f; // Cooldown after charge
    private enum EnemyState { Approaching, Pausing, Charging, Cooldown, Dying }
    private EnemyState currentState = EnemyState.Approaching;
    public float chargeDrag = 1.5f; // Adjust for smoother slowdown

    //variables related to enemy movement
    private Transform target;
    private Vector2 direction;
    private Rigidbody2D rb;

    //variables related to enemy rotation
    private SpriteRenderer cursprite;
    [SerializeField] Sprite[] sprites;
    //0 = back 1 = right 2 = front 3 = left

    //reference to wavespawner
    wavespawner ws;

    //reference to ui to add score


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
            else if (collision.gameObject.tag == "Enemy")
            {
                // gets the script of the enemy it collided. this might be redundant idk
                enemy EnemyScript = collision.gameObject.GetComponent<enemy>();
                EnemyScript.Die(); //kills the other enemy that its collieded
            }
            else if (collision.gameObject.tag == "Ranged_enemy")
            {
                ranged_enemy rangedEnemy = collision.gameObject.GetComponent<ranged_enemy>();
                rangedEnemy.Die(); //kills the other enemy that its collieded
            }
        }



    }

    public void Die()
    {
        currentState = EnemyState.Dying;
        // Add 100 points to the score
        UIscript.Instance.AddScore(100);

        // Turn red
        cursprite.color = Color.red;

        StartCoroutine(FallOver());
        // "Fall over" by rotating 90 degrees (in 2D space, z-axis)
        transform.rotation = Quaternion.Euler(0, 0, 90);

        // Disable enemy behavior (we hope)
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

        //Destroy after a delay
        Destroy(gameObject, 0.50f);
    }
    private IEnumerator FallOver()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, -90);

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRot;
    }

    private void OnDestroy()
    {
        ws.enemiesLeft--;
    }


    private void ApproachPlayer()
    {
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
}
