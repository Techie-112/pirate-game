using UnityEngine;

public class enemy : MonoBehaviour
{
    //the enemy's stats
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public int damage = 1;
    player player;
    
    //variables related to enemy movement
    private Transform target;
    private Vector2 direction;
    private float angle;
    private Rigidbody2D rb2d;

    //variables related to enemy rotation
    private SpriteRenderer cursprite;
    [SerializeField] Sprite[] sprites;
    //0 = back 1 = right 2 = front 3 = left
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        cursprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        direction = (target.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        moveEnemy();
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


    private void moveEnemy()
    {
        /*this.angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //get angle between current rotation and target position
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //rotate towards the target
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed); */

        //move towards the player
        this.rb2d.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
        
        //decide which way they are facing
        if (rb2d.linearVelocityX > rb2d.linearVelocityY)
        {
            if (rb2d.linearVelocityX >= 0)
            {
                //going right
                cursprite.sprite = sprites[1];
            } else
            {
                //going left
                cursprite.sprite = sprites[3];
            }
        } else
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
