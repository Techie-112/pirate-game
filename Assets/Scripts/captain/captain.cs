using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class captain : MonoBehaviour
{

    //captain stats
    public bool inRange = false;
    public float atkRange;

    //animator
    Animator anim;


    //references to other objects
    player player;

    Transform playerPos;
    Rigidbody2D rb2d;

    //other captain objects
    public waterboarding water;
    Vector3 waterpos;
    public spit spitball;



    private void Start()
    {
        //i dont know why setting the attack range here works but im not complaining
        atkRange = 1.1f;

        //find the other gameobjects
        player = GameObject.FindWithTag("Player").GetComponent<player>();
        playerPos = player.transform;

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        waterpos = new Vector3(0,3f,0f);

    }

    private void Update()
    {
        anim.SetFloat("targX", player.transform.position.x);
        anim.SetFloat("targY", player.transform.position.y);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        { inRange = true; }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        { inRange = false; }
    }

    public void chase(float spd)
    {
        
        //get the player's location
        Vector2 target = new Vector2(playerPos.position.x, playerPos.position.y);

        //shimmy the captain towards the player
        //param 1 takes current position, param 2 takes player position, param 3 is movement speed
        rb2d.MovePosition(Vector2.MoveTowards(rb2d.position, target, spd * Time.deltaTime));

        if (Vector2.Distance(playerPos.position, rb2d.position) < atkRange)
        { inRange = true; }
        else
        { inRange = false; }

    }

    //when the animations are done ill fuse the bite & stab together to create one "damage player"

    public void damagePlayer()
    {
        if (inRange)
        {
            player.TakeDamage();
        }
    }

    public void stab()
    {
        //player is on left if this is less than zero

        if (inRange)
        {
            player.TakeDamage();
            //tentacle.SetActive(false);
        }
    }

    //literally just copy pasting the ranged enemy attack
    public void shoot()
    {

        Debug.Log("pewpew");
        Instantiate(spitball, transform.position, Quaternion.identity);
    }


    public void woosh()
    {

        Instantiate(water, waterpos, transform.rotation);

        anim.SetTrigger("waterboard");
    }
}
