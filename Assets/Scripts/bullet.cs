using UnityEngine;

public class bullet : MonoBehaviour
{
    public float muzzle_velocity = 10f;
    private Rigidbody2D rgbd2D;


    // Start is called before the first frame update
    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rgbd2D.linearVelocity = transform.TransformDirection(Vector3.right) * muzzle_velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ranged_enemy")
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
}