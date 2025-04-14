using Unity.VisualScripting;
using UnityEngine;

public class spit : MonoBehaviour
{
    public float muzzle_velocity = 10f;
    private Vector2 direction;
    Vector3 startPoint;
    public player player;
    public Transform target;
    public puddle puddle;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        startPoint = transform.position;
        target = player.transform;
        direction = (target.position - transform.position);
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, direction, muzzle_velocity * Time.deltaTime);
        if (Vector3.Dot((target.position - startPoint).normalized, (transform.position - target.position).normalized) > 0) 
        {
            Destroy(this.gameObject);
        }

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
