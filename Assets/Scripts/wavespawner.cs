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
    private int wavenum;

    private float nextWave = 0f;
    private float nextEnemy;

    private bool canSpawn = true;



    private void Update()
    {
        curwave = waves[wavenum];

        if (Time.time > nextWave && wavenum <= waves.Length - 1)
        {
            spawnWave();         
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
