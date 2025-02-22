using UnityEngine;
public class player : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private SpriteRenderer cursprite;
    [SerializeField] Sprite[] sprites;
    //0 = back 1 = right 2 = left 3 = front

    public Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cursprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        facing();
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
}
