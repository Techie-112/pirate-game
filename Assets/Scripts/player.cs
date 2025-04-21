using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine.UI;
public class player : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public Camera cam;
    private SpriteRenderer cursprite;
    [SerializeField] Sprite[] sprites;
    //0 = back 1 = right 2 = front 3 = left

    public int maxLives = 6; // Total lives the player has
    public int currentLives;

    public float invincibilityDuration = 1f; // Time of invincibility after getting hit
    private bool isInvincible = false;

    public UIscript UI;
    public GameObject pushHitbox;
    public float pushDuration = 0.3f;
    public float pushCooldown = 8f; // in seconds
    private bool canPush = true;
    public Image pushIndicator; // drag your UI square here in the Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cursprite = GetComponent<SpriteRenderer>();
        UI = GameObject.FindGameObjectWithTag("UItag").GetComponent<UIscript>();
        pushIndicator = GameObject.FindGameObjectWithTag("thesquare").GetComponent<Image>();

        currentLives = maxLives;

    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        //handles player sprite direction based on mouse cursor
        facing();

        if (Input.GetKeyDown(KeyCode.K))
        {
            currentLives++;
            Debug.Log("current lives: " + currentLives);
        }
        if (Input.GetMouseButtonDown(0) && canPush)
        {
            StartCoroutine(DoPush());
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            print("manually going next scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator DoPush()
    {
        canPush = false;
        pushIndicator.color = Color.black;

        pushHitbox.SetActive(true);
        yield return new WaitForSeconds(0.1f); // duration of push hitbox
        pushHitbox.SetActive(false);

        yield return new WaitForSeconds(pushCooldown); // cooldown period
        canPush = true;
        pushIndicator.color = Color.white;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }




    void facing()
    {
        //get the mouse position from the center of the screen
        //this is because using input.mouseposition on its own
        //returns a value using the bottom left corner of the scene as (0,0)
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        //get the distance between the player and the mouse
        Vector2 dist = mousePos - transform.position;
        
        //use that distance to calculate the angle between the mouse and the player
        //then convert into degrees
        float angle = Mathf.Atan2(dist.y, dist.x)*Mathf.Rad2Deg;

        if (angle >= 45 && angle <= 135)
        { cursprite.sprite = sprites[0]; }
        else if (angle >= -135 && angle <= -45)
        { cursprite.sprite = sprites[2]; }
        else if (angle <= 45 && angle >= -45)
        { cursprite.sprite = sprites[1]; }
        else if (angle >= 135 || angle <= -135)
        { cursprite.sprite = sprites[3]; }

    }

    public void TakeDamage()
    {
        if (isInvincible) return; // Ignore damage if invincible

        currentLives--; // Reduce lives

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
        Debug.Log("Player lives: " + currentLives);
    }
    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        float blinkTime = 0.1f; // Blink speed

        for (float i = 0; i < invincibilityDuration; i += blinkTime)
        {
            cursprite.enabled = !cursprite.enabled; // Toggle visibility
            yield return new WaitForSeconds(blinkTime);
        }

        cursprite.enabled = true;
        isInvincible = false;
    }
    private void Die()
    {
        Debug.Log("Player Died");
        UI.GameOver();
        Destroy(gameObject);
    }
}
