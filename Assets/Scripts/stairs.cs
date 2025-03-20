using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stairs : MonoBehaviour
{
    //reference to wavespawner
    wavespawner ws;

    bool canLeave = false;

    private void Start()
    {
        ws = GameObject.FindWithTag("ws").GetComponent<wavespawner>();
    }
    private void Update()
    {
        if (ws.enemiesLeft == 0 && ws.wavenum == ws.waves.Length)
        { canLeave = true; }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && canLeave == true)
        {
            print("beep boop");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
        
}
