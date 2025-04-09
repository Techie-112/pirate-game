using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class captain : MonoBehaviour
{

    //captain stats
    public bool inRange = false;
    public float biteRange;
    public float speed = 1.0f;

    //references to other objects
    player player;

    Transform playerPos;
    Rigidbody2D rb2d;

    //other captain objects
    public GameObject tentacle;
    public waterboarding water;
    public Transform[] waterpos;

    private void Start()
    {
        //i dont know why setting biterange here works but im not complaining
        biteRange = 1.0f;

        //find the other gameobjects
        player = GameObject.FindWithTag("Player").GetComponent<player>(); 
        playerPos = GameObject.FindWithTag("Player").GetComponent<player>().transform;
        rb2d = GetComponent<Rigidbody2D>();

        //set tentacle to inactive until it's needed to attack
        tentacle = GameObject.Find("tentacle");
        tentacle.SetActive(false);

        //set water stuff
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        { inRange = true; }
    }

    public void move()
    {
        
        //get the player's location
        Vector2 target = new Vector2(playerPos.position.x, playerPos.position.y);

        //shimmy the captain towards the player
        //param 1 takes current position, param 2 takes player position, param 3 is movement speed
        rb2d.MovePosition(Vector2.MoveTowards(rb2d.position, target, speed * Time.deltaTime));

        if (Vector2.Distance(playerPos.position, rb2d.position) < biteRange)
        { inRange = true; }
        else
        { inRange = false; }

    }

    public void bite()
    {
        if (inRange)
        {
            player.currentLives = 0;
        }
    }

    public void stab()
    {
        //player is on left if this is less than zero
        if (player.transform.position.x - transform.position.x < 0)
        { tentacle.transform.localScale = new Vector3(-2.5f, 1f, 1f); }
        //otherwise they are on right
        else
        { tentacle.transform.localScale = new Vector3(2.5f, 1f, 1f); }

        
        tentacle.SetActive(true);

        if (inRange)
        {
            player.currentLives--;
            //tentacle.SetActive(false);
        }
    }
    public void swing()
    {
        //for some reason swing isnt working so that's inconvenient
        print("beepboop");
        //if the player is above the enemy, swing upwards
        if (player.transform.position.y > transform.position.y)
        {
            tentacle.transform.Rotate(new Vector3(0f, 0f, 90f));
        }
        //otherwise swing downwards
        else
        {
            tentacle.transform.Rotate(new Vector3(0f, 0f, -90f));
        }

        tentacle.SetActive(false);
    }

    public void woosh()
    {
        Transform randomPoint = waterpos[Random.Range(0, waterpos.Length)];
        Instantiate(water, randomPoint.position, Quaternion.identity);
    }
}
