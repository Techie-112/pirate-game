using UnityEngine;

public class Push_enemies : MonoBehaviour
{
    bool in_range = false;
    private Rigidbody2D enemyRB;

    public float Force = 500f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        in_range = true;
        //Debug.Log(collision.tag);
        Push(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        in_range = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Push(collision);
    }

    private void Push(Collider2D collision)
    {
        Vector3 direction = transform.position - collision.transform.position;
        direction.Normalize();
        enemyRB = collision.GetComponent<Rigidbody2D>();
        enemyRB.AddForce(direction * Force);

    }
}