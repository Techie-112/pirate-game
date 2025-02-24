using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;

    private Transform player;
    private Vector2 direction;
    private float angle;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        direction = (player.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        moveEnemy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Wall")
        { Destroy(collision.gameObject); }


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
        //i kept getting a warning saying that rb2d.velocity was obsolete, so i changed it to linearVelocity -max

        if (rb2d.linearVelocityX > rb2d.linearVelocityY)
        {
            if (rb2d.linearVelocityX >= 0)
            {
                // assign going right animation
            } else
            {
                // assign going left animation
            }
        } else
        {
            if (rb2d.linearVelocityY >= 0)
            {
                // assign going up animation
            }
            else
            {
                // assign going down animation
            }
        }

    }
}
