using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 0.3f;
    public float rotationSpeed = 5f;
    private Vector2 direction;
    private float angle;
    private Rigidbody2D rb2d;

    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        this.direction = (player.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        this.angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //get angle between current rotation and target position
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //rotate towards the target
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

        //move towards the player
        this.rb2d.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
        //i kept getting a warning saying that rb2d.velocity was obsolete, so i changed it to linearVelocity -max
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
