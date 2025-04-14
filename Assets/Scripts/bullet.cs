using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class bullet : MonoBehaviour
{
    public float muzzle_velocity = 10f;
    private Rigidbody2D rgbd2D;
    private Vector2 direction;
    public float angle;
    public Transform player;

    public GameObject shooter; // Assign this when instantiating

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        rgbd2D.linearVelocity = direction * muzzle_velocity;
    }

    private void Awake()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = (player.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != shooter || collision.gameObject.tag != "Wall")
        {
            if (collision.gameObject.tag == "Player")
            {
                player playerScript = collision.gameObject.GetComponent<player>();
                playerScript.TakeDamage();
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                enemy EnemyScript = collision.gameObject.GetComponent<enemy>();
                EnemyScript.Die(); //kills the other enemy that its collieded
            }
            else if (collision.gameObject.tag == "Ranged_enemy")
            {
                ranged_enemy rangedEnemy = collision.gameObject.GetComponent<ranged_enemy>();
                rangedEnemy.Die(); //kills the other enemy that its collieded
            }
        }
        Destroy(gameObject);
        Debug.Log("bullet collided with: " + collision.gameObject.name);
    }
}