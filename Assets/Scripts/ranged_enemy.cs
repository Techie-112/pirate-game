using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using System.Runtime.CompilerServices;
using static UnityEngine.GraphicsBuffer;

public class ranged_enemy : MonoBehaviour
{
    public Transform Player;
    public float speed = 0.3f;
    private Vector2 direction;
    private Rigidbody2D rb2d;
    private bool can_move = true;
    public float targetTime;
    public GameObject Bullet;
    private float angle;
    private float initialTargetTime;

    [SerializeField] Sprite[] sprites;
    private SpriteRenderer cursprite;

    //reference to wavespawner
    wavespawner ws;

    
    //melee enemy stuff
    //the enemy's stats
    public float rotationSpeed = 5f;
    public int damage = 1;

    //variables related to enemy movement
    private Transform target;
    //private Vector2 direction;
    private Rigidbody2D rb;
    private enum EnemyState { Approaching, Pausing, Charging, Cooldown, Dying }
    private EnemyState currentState = EnemyState.Approaching;

    //flash white
    private Renderer spriteRenderer;
    private Color originalColor;
    public Material flashMaterial;
    private Material originalMaterial;


    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();

        ws = GameObject.FindWithTag("ws").GetComponent<wavespawner>();
        cursprite = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        ws = GameObject.FindWithTag("ws").GetComponent<wavespawner>();

        initialTargetTime = targetTime;

        originalColor = cursprite.color;
        originalMaterial = cursprite.material;
    }

    // Update is called once per frame
    void Update()
    {

        if (rb2d.linearVelocityX > rb2d.linearVelocityY && rb2d.linearVelocityX > 0)
        { cursprite.sprite = sprites[0]; }
        else if (rb2d.linearVelocityY < rb2d.linearVelocityX && rb2d.linearVelocityY < 0)
        { cursprite.sprite = sprites[2]; }
        else if (rb2d.linearVelocityX < rb2d.linearVelocityY && rb2d.linearVelocityX > 0)
        { cursprite.sprite = sprites[1]; }
        else if (rb2d.linearVelocityX < rb2d.linearVelocityY && rb2d.linearVelocityX > 0)
        { cursprite.sprite = sprites[3]; }

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.00)
        {
            targetTime = initialTargetTime; //reset timer           
            StartCoroutine(FlashAndFire());
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            cursprite.color = Color.white;
        }
    }

    private void FixedUpdate()
    {
        direction = (Player.position - transform.position).normalized;

        angle = Mathf.Atan2(direction.x, direction.y);
        //get angle between current rotation and targets position
        //Quaternion Target_rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //rotate towards the target
        //transform.right = Player.position - transform.position;


        if (can_move)
        {
            // move towards the player
            rb2d.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
        }




        if (currentState == EnemyState.Approaching)
        {
            //ApproachPlayer();
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
        }



    }

    public void OnDestroy()
    {
        ws.enemiesLeft--;
    }
    protected IEnumerator FlashWhite(float duration)
    {
        float ff = 3;
        Debug.Log("Started Flash White");
        cursprite.color = Color.cyan;
        //its cyan because white doesnt work because it's already white. I am very mad about this
        yield return new WaitForSeconds(duration);
        cursprite.color = originalColor;
    }
    /*IEnumerator FlashWhiteMaterial(float duration = 0.1f)
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = originalMaterial;
    }
    */
    protected IEnumerator FlashAndFire()
    {
        Debug.Log("started F&F");
        yield return StartCoroutine(FlashWhite(0.3f));
        // StartCoroutine(FlashWhiteMaterial(0.5f));
        // You can also play a wind-up animation here
        yield return new WaitForSeconds(0.4f); // Delay before attack
        shoot(); // Call your attack method
    }

    public void shoot()
    {
        
        Debug.Log("Fired");
        // create projectile with ranged enemies position and rotation 
        //Quaternion bulletRotation = transform.rotation * Quaternion.Euler(0, 0, 90);
        GameObject bullet = Instantiate(Bullet, transform.position /*+ (transform.right * 1.5f)*/, transform.rotation);
        bullet bulletScript = bullet.GetComponent<bullet>();
        bulletScript.shooter = gameObject; // Reference to the shooter
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

    void facing()
    {
        if (rb2d.linearVelocityX > rb2d.linearVelocityY)
        {
            if (rb2d.linearVelocityX >= 0)
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
            if (rb2d.linearVelocityY >= 0)
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
}