using UnityEngine;

public class ArcPusher : MonoBehaviour
{
    public float pushForce = 10f;
    public float radius = 1.5f; // Distance from player

    private Transform player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 pushDirection = (other.transform.position - transform.position).normalized;
                enemyRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }

    void Start()
    {
        player = transform.parent; // Assumes this is a child of the player
    }

    void Update()
    {
        // Get mouse position in world space
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        // Get direction from player to mouse
        Vector3 direction = (mouseWorldPos - player.position).normalized;

        // Set position at radius distance in that direction
        transform.position = player.position + direction * radius;

        // Rotate to face the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
