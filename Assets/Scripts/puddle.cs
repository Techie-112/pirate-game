using UnityEngine;

public class puddle : MonoBehaviour
{
    player player;
    public Transform respawnPoint;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<player>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player) 
        { 
            player.TakeDamage();
        }
    }
}
