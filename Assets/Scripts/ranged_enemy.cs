using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ranged_enemy : MonoBehaviour
{
    public Transform Player;
    public float speed = 0.3f;
    private Vector2 direction;
    private Rigidbody2D rb2d;
    private bool can_move = true;
    public float targetTime = 3.0f;
    public GameObject Bullet;
    public float move_time = 3f;
    private GameObject game_object;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
        game_object = GetComponent<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {

        //get target position difference

    }

    private void FixedUpdate()
    {
        direction = (Player.position - transform.position).normalized;

        
        //get angle between current rotation and targets position      
        move_time -= Time.deltaTime;

        if (can_move)
        {
            // move towards the player
            rb2d.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
        }


        if (move_time <= 0.00)
        {
            //rotate towards the target
            
            transform.forward = Vector3.forward;
            rb2d.linearVelocity = new Vector2(0, 0);
            can_move = false;
            targetTime -= Time.deltaTime;
            if (targetTime <= 0)
            {
                shoot();
            }

        }

        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(collision.gameObject);
    }

    public void shoot()
    {
        Debug.Log("Fired");
        // create projectile with ranged enemies position and rotation 
        Instantiate(Bullet, transform.position, transform.rotation);
        targetTime = 3f;
        move_time = 2f;
        can_move = true;
    }
}