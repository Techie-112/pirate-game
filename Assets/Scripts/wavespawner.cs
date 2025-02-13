using UnityEngine;
[System.Serializable]


public class wave
{
    public GameObject[] enemyList;

    public float enemyInterval;
    public float spawnInterval;

}

public class wavespawner : MonoBehaviour
{
    public wave[] waves;
    public Transform[] spawnpoints;

    private wave curwave;
    private int wavenum;

    private float nextWave;
    private float nextEnemy;

    private bool canSpawn = true;



    private void Update()
    {
        curwave = waves[wavenum];
        for (int i = 0; i < waves.Length - 1; i++)
        {
            spawnWave();         
        }
    }

    private void spawnWave()
    {
        if (canSpawn && nextWave < Time.time)
        {
        
            spawnEnemy();
            nextEnemy = Time.time + curwave.enemyInterval;

            canSpawn = false;
        }
        else if (!canSpawn && wavenum < waves.Length - 1)
        {
            nextWave = Time.time + curwave.spawnInterval;
            wavenum++;
            canSpawn = true;
            
        }
    }

    private void spawnEnemy() 
    {

        for (int i = 0; i < curwave.enemyList.Length; i++)
        {
            if (nextEnemy < Time.time)

            {
                Transform randomPoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
                Instantiate(curwave.enemyList[i], randomPoint.position, Quaternion.identity);
                //nextEnemy = Time.time + curwave.enemyInterval;
            }

        }

    }
    }
