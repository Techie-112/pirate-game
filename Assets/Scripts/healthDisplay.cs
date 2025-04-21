using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    public Sprite empty;
    public Sprite half;
    public Sprite full;
    public Image[] hearts;

    public player player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    }
    void Update()
    {
        //local variable to be subtracted
        int livesLeft = player.currentLives;
        
        //basically takes the 6 lives and divides them up between the hearts
        for (int i = 0; i < hearts.Length; i++)
        {
            if (livesLeft >= 2)
            {
                hearts[i].sprite = full;
                livesLeft -= 2;
            }
            else if (livesLeft == 1)
            {
                hearts[i].sprite = half;
                livesLeft -= 1;
            }
            else
            {
                hearts[i].sprite = empty;
            }
        }

    }
}
