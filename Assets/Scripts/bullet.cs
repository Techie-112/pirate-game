using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class bullet : MonoBehaviour
{
    public float muzzle_velocity = 10f;
    private Rigidbody2D rgbd2D;
    private Vector2 direction;
    public float angle;
    public Transform Player;


    // Start is called before the first frame update
    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = (Player.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.right = Player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        rgbd2D.linearVelocity = transform.TransformDirection(Vector3.right) * muzzle_velocity;
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
        Destroy(this.gameObject);
    }
}