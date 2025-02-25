using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Sprite empty;
    public Sprite full;
    public Image[] hearts;

    public player player;
    
    void Update()
    {
        health = player.currentLives;
        maxHealth = player.maxLives;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            { hearts[i].sprite = full; }
            else
            { hearts[i].sprite = empty; }

            if (i < maxHealth)
            { hearts[i].enabled = true; }
            else
            { hearts[i].enabled = false; }

        }

    }
}
