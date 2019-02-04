using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPCSpawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnerPrefabs;
    private GameObject enemyForSpawn;
    public GameObject[] allEnemyTab;
    private BonusMenu bonusMenu;
    private EnemyMenu enemyMenu;

    [Range(10, 30)] [SerializeField] private int maxEnemyInGame = 20;

    private int count;

    private void Start()
    {
        if (allEnemyTab.Length > 0)
        {
            enemyForSpawn = allEnemyTab[enemyMenu.currentEnemyIcon];
            count = enemyMenu.currentEnemyIcon;
        }
    }

    public IEnumerator Spawn(RaycastHit _hit, GameObject _enemyForSpawn, int _count)
    {
        Vector3 tmp = new Vector3(0f, 0.66f, 0f);
        GameObject spawnerIns_ = Instantiate(spawnerPrefabs, _hit.point + tmp, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        if (GameManager.Instance.enemyInsList.Count <= maxEnemyInGame)
        {
            GameObject enemyIns_ = Instantiate(_enemyForSpawn, spawnerIns_.transform.position, spawnerIns_.transform.rotation);
            enemyIns_.GetComponent<EnemyStats>().SetID(_count);
            bonusMenu.GetEnemyIns(enemyIns_);
            GameManager.Instance.enemyInsList.Add(enemyIns_);
        }
    }

    public void ChangeEnemy(int count_)
    {
        count = count_;
        if (allEnemyTab.Length > 0)
        {
            enemyForSpawn = allEnemyTab[count_];
        }
    }

    public void SetUI(GameObject _UI)
    {
        bonusMenu = _UI.GetComponent<BonusMenu>();
        enemyMenu = _UI.GetComponent<EnemyMenu>();
    }

    public int GetCount()
    {
        return count;
    }

    public GameObject GetEnemySpawn()
    {
        return enemyForSpawn;
    } 
}
