using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuButtons : MonoBehaviour
{
    Button button;
    Color32 color;
    
    public void Start()
    {
        button = GetComponent<Button>();

        //universal color duller
        //makes it so that the highlight tint applies to all buttons
        //as opposed to going in one by one and changing it manually
        //0 = 
        color.r = 150; color.g = 150; color.b = 150;
        //alpha channel deals with transparancy
        //0 = transparent, 255 = fully opaque
        color.a = 255;

        ColorBlock cb = button.colors;
        cb.highlightedColor = color;
        button.colors = cb;

    }
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Exited game");
    }

    public void hover()
    {
        transform.localScale = new Vector3(1.25f, 1.25f, 1.0f);      
    }

    public void leave()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
