using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private PlayerPCSpawner playerPCSpawner;

    // Liste des ennemies que le spawner va faire spawn
    private List<GameObject> enemySpawnList;
    // Temps en seconde entre le spawn de deux ennemies
    [SerializeField] [Range(1f, 5f)] private float timeBtwTwoSpawn = 2f;

    private void Awake()
    {
        enemySpawnList = new List<GameObject>();
    }

    public void AddEnemyToList(List<GameObject> _enemyList)
    {
        foreach (GameObject enemy in _enemyList)
        {
            enemySpawnList.Add(enemy);
        }
    }

    public IEnumerator SpawnEnemy()
    {
        foreach (GameObject enemy in enemySpawnList)
        {
            Instantiate(enemy, this.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(timeBtwTwoSpawn);
        }
        playerPCSpawner.DestroySpawner(this.gameObject);
    }
}
