using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ranged_enemy : MonoBehaviour
{
    public Transform Player;
    public float speed = 0.3f;
    private Vector2 direction;
    private Rigidbody2D rb2d;
    private bool can_move = true;
    public float targetTime = 120.0f;
    public GameObject Bullet;
    private float angle;

    //reference to wavespawner
    wavespawner ws;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();

        ws = GameObject.FindWithTag("ws").GetComponent<wavespawner>();

    }

    // Update is called once per frame
    void Update()
    {

        //get target position difference

    }

    private void FixedUpdate()
    {
        direction = (Player.position - transform.position).normalized;

        angle = Mathf.Atan2(direction.x, direction.y);
        //get angle between current rotation and targets position
        Quaternion Target_rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //rotate towards the target
        transform.right = Player.position - transform.position;

        targetTime -= Time.deltaTime;

        if (can_move)
        {
            // move towards the player
            rb2d.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
        }


        if (targetTime <= 0.00)
        {
            targetTime = 120f;
            shoot();
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(collision.gameObject);
    }

    public void OnDestroy()
    {
        ws.enemiesLeft--;
    }

    public void shoot()
    {
        
        Debug.Log("Fired");
        // create projectile with ranged enemies position and rotation 
        //Quaternion bulletRotation = transform.rotation * Quaternion.Euler(0, 0, 90);
        Instantiate(Bullet, transform.position + (transform.right * 1.5f), transform.rotation);
    }
}