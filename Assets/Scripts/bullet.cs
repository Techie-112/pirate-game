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
        if (collision.gameObject.tag != "Ranged_enemy" || collision.gameObject.tag != "Wall")
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
        Destroy(gameObject);
    }
}