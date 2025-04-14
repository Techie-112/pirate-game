using UnityEngine;

public class puddle : MonoBehaviour
{
    public player player;
    public captain captain;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<player>();
        captain = GameObject.FindWithTag("captain").GetComponent<captain>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "wall")
        {
            if (collision.gameObject.tag == "Player")
            {
                player.TakeDamage();
            }
            else if (collision.gameObject.tag == "captain")
            {
                //captain takes damage;
            }
            else if (collision.gameObject.tag == null)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
