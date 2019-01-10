using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private PlayerPCSpawner playerPCSpawner;

    private List<GameObject> enemyPrefabsList;
    [SerializeField] [Range(1f, 5f)] private float timeBtwTwoSpawn = 2f;

    // Use this for initialization
    private void Start()
    {
        enemyPrefabsList = new List<GameObject>();
    }

    public void AddEnemyInList(List<GameObject> enemyList_)
    {
        foreach (GameObject enemy in enemyList_)
        {
            enemyPrefabsList.Add(enemy);
        }
        SpawnEnemy();
    }

    private IEnumerator SpawnEnemy()
    {
        foreach (GameObject enemy in enemyPrefabsList)
        {
            Debug.Log(enemy.name);
            Instantiate(enemy, this.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(timeBtwTwoSpawn);
        }
        playerPCSpawner.DestroySpawner(this.gameObject);
    }
}
