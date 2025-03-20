using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    public Sprite empty;
    public Sprite full;
    public Image[] hearts;

    public player player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    }
    void Update()
    {

        for (int i = 0; i < player.maxLives; i++)
        {
            if (i < player.currentLives)
            { hearts[i].overrideSprite = full; }
            else
            { hearts[i].overrideSprite = empty; }

            if (i < player.maxLives)
            { hearts[i].enabled = true; }
            else
            { hearts[i].enabled = false; }

        }

    }
}
