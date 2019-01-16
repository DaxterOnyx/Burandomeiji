using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPCSpawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnerPrefabs;
    private GameObject enemyForSpawn;
    public GameObject[] allEnemyTab; 

    private void Start()
    {
        if (allEnemyTab.Length > 0)
        {
            enemyForSpawn = allEnemyTab[0];
        }
    }

    public IEnumerator Spawn(RaycastHit _hit)
    {
        GameObject spawnerIns_ = Instantiate(spawnerPrefabs, _hit.point, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        GameObject enemyIns_ = Instantiate(enemyForSpawn, spawnerIns_.transform.position, spawnerIns_.transform.rotation);
        Destroy(spawnerIns_, 1.5f);
    }

    public void ChangeEnemy(int count_)
    {
        if (allEnemyTab.Length > 0)
        {
            enemyForSpawn = allEnemyTab[count_];
        }
    }
}
