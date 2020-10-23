using System.Collections.Generic;
using UnityEngine;
using Tools;
using System;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    private Pathfinding path;
    private List<Vector3> list;
    private GameObjectPool smallEnemyPool;
    private GameObjectPool bigEnemyPool;
    private List<string> enemies;
    private MapReader mapReader;
    private MapReaderMono mapReaderMono;
    private GameManager gameManager;

    [SerializeField] private GameObject m_smallEnemyPrefab;
    [SerializeField] private GameObject m_bigEnemyPrefab;
    [SerializeField] private float m_enemySpawnIntervall = 1f;
    [SerializeField] private float m_waveSpawnIntervall = 1f;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        mapReaderMono = GetComponent<MapReaderMono>();
        
        mapReader = new MapReader();

        enemies = new List<string>();
        enemies = mapReader.ReadEnemyWaves(mapReaderMono.TxtFile);

        path = GetComponent<Pathfinding>();
        list = path.WorldPos;

        if (m_smallEnemyPrefab != null)
        {
            smallEnemyPool = new GameObjectPool(10, m_smallEnemyPrefab);
        }
        if (m_bigEnemyPrefab != null)
        {
            bigEnemyPool = new GameObjectPool(10, m_bigEnemyPrefab);
        }

        gameManager.GetMaxWave(enemies.Count);

        StartCoroutine("ReadInEnemies");
    }

    IEnumerator ReadInEnemies()
    {
        //Spawning enemies in order but with waves instead
        for (int i = 0; i < enemies.Count; i++) //Goes through all enemy lines in the map file (under the #), each line is one wave
        {
            int enemyType = 0;

            foreach (string item in enemies[i].Split()) //Get each number on the line. Splitting the string into all strings found, getting each number for each enemy 
            {
                int num = Int32.Parse(item); //Converting the string to an int
               
                for (int currentNum = 0; currentNum < num; currentNum++) //Spawn x amount of enemies based on the number we read from the string
                {
                    CreateEnemy(enemyType % 2); //Determine which type of enemy to spawn, 

                    yield return new WaitForSeconds(m_enemySpawnIntervall);
                }

                enemyType++; //Since we now went through the first element in the string, we increase the int since the next string being read is the other enemy type
            }
            
            yield return new WaitForSeconds(m_waveSpawnIntervall);
            
            gameManager.UpdateWaveUI();
        }
        yield return null;
    }

    public void CreateEnemy(int num)
    {
        GameObject enemy = null;

        if (num == 0)
        {
            enemy = smallEnemyPool.Rent(true);
        }
        else if (num == 1)
        {
            enemy = bigEnemyPool.Rent(true);
        }
        enemy.transform.position = list[0];
    }
}