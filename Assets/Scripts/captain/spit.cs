using UnityEngine;

public class spit : MonoBehaviour
{
    public float muzzle_velocity = 10f;
    private Rigidbody2D rgbd2D;
    private Vector2 direction;
    public float angle;
    public player player;
    public Transform target;
    public puddle puddle;


    // Start is called before the first frame update
    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        target = player.transform;
        direction = (target.position - transform.position);
    }

    void FixedUpdate()
    {
        //rgbd2D.linearVelocity = transform.TransformDirection(direction);

        transform.position = Vector2.MoveTowards(transform.position, direction, muzzle_velocity * Time.deltaTime);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "captain")
        {
            if (collision.gameObject.tag == "Player")
            {
                player playerScript = collision.gameObject.GetComponent<player>();
                playerScript.TakeDamage();
            }
        }
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(puddle, transform.position, Quaternion.identity);        
    }
}
