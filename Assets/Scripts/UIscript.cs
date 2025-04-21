using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIscript : MonoBehaviour
{
    public static UIscript Instance; // Singleton instance

    public int score = 0;
    public GameObject gameOverScreen;
    public Text scoreText;

    void Awake()
    {
        // Basic singleton setup
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Prevent duplicates
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
    public void RestartLevel()
    {
        Debug.Log("restarting level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
