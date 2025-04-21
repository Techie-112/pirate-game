using UnityEngine;

public class puddle : MonoBehaviour
{
    public player player;
    public captain captain;
    public enemy enemy;
    public ranged_enemy ranged;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<player>();
        captain = GameObject.FindWithTag("captain").GetComponent<captain>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       print(collision.gameObject.tag);



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
            else if (collision.gameObject.tag == "Enemy")
            {
                enemy = collision.gameObject.GetComponent<enemy>();
                enemy.Die();
            }
            else if (collision.gameObject.tag == "Ranged_enemy")
            {
                ranged = GameObject.FindWithTag("Ranged_enemy").GetComponent<ranged_enemy>();
                ranged.Die();
            }


            Destroy(this.gameObject);
        }
    }
}
