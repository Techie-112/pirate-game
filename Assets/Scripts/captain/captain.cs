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

    //variables related to enemy rotation
    public SpriteRenderer cursprite;
    public Sprite[] sprites;
    //0 = back 1 = right 2 = front 3 = left

    private void Start()
    {
        //i dont know why setting biterange here works but im not complaining
        biteRange = 1.0f;

        //find the other gameobjects
        player = GameObject.FindWithTag("Player").GetComponent<player>(); 
        playerPos = GameObject.FindWithTag("Player").GetComponent<player>().transform;
        rb2d = GetComponent<Rigidbody2D>();
        cursprite = GetComponent<SpriteRenderer>();


        //set tentacle to inactive until it's needed to attack
        tentacle = GameObject.Find("tentacle");
        tentacle.SetActive(false);

        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        { inRange = true; }
    }

    public void move(float spd)
    {
        
        //get the player's location
        Vector2 target = new Vector2(playerPos.position.x, playerPos.position.y);

        //shimmy the captain towards the player
        //param 1 takes current position, param 2 takes player position, param 3 is movement speed
        rb2d.MovePosition(Vector2.MoveTowards(rb2d.position, target, spd * Time.deltaTime));
        facing();

        if (Vector2.Distance(playerPos.position, rb2d.position) < biteRange)
        { inRange = true; }
        else
        { inRange = false; }

    }

    public void facing()
    {
        //get distance between enemy and player
        Vector2 dist = player.transform.position - transform.position;
        //get the angle
        float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;

        print(angle);

        //decide which way the captain is facing
        if (angle >= 45 && angle <= 135)
        { cursprite.sprite = sprites[0]; }
        else if (angle >= -135 && angle <= -45)
        { cursprite.sprite = sprites[2]; }
        else if (angle <= 45 && angle >= -45)
        { cursprite.sprite = sprites[1]; }
        else if (angle >= 135 || angle <= -135)
        { cursprite.sprite = sprites[3]; }
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
