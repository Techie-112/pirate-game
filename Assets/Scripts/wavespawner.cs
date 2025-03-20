using UnityEngine;
[System.Serializable]


public class wave
{
    public GameObject[] enemyList;
    public int enemyIndex = 0;

    public float enemyInterval;
    public float spawnInterval;

}

public class wavespawner : MonoBehaviour
{
    public wave[] waves;
    public Transform[] spawnpoints;

    private wave curwave;
    public int wavenum;

    private float nextWave = 0f;
    private float nextEnemy;

    private bool canSpawn = true;

    public int enemiesLeft;

    private void Update()
    {

        if (wavenum < waves.Length)
        {
            curwave = waves[wavenum];

            if (Time.time > nextWave)
            {
                spawnWave();
            }
        }
    }

    private void spawnWave()
    {
        if (canSpawn && nextEnemy < Time.time)
        {      
            spawnEnemy();
        }
        else if (!canSpawn)
        {
            nextWave = Time.time + curwave.spawnInterval;
            wavenum++;
            canSpawn = true;
            
        }
    }

    private void spawnEnemy() 
    {
        enemiesLeft++;
        print(enemiesLeft);
        Transform randomPoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
        Instantiate(curwave.enemyList[curwave.enemyIndex], randomPoint.position, Quaternion.identity);
        
        curwave.enemyIndex++;
        nextEnemy = Time.time + curwave.enemyInterval;
        

        if (curwave.enemyIndex == curwave.enemyList.Length)
        {
            canSpawn = false;
        }
    }

}
