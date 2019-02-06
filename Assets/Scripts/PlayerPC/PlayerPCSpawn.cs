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
        AddOneEnemy(_enemyForSpawn);
        Vector3 tmp = new Vector3(0f, 0.66f, 0f);
        GameObject spawnerIns_ = Instantiate(spawnerPrefabs, _hit.point + tmp, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        GameObject enemyIns_ = Instantiate(_enemyForSpawn, spawnerIns_.transform.position, spawnerIns_.transform.rotation);
        enemyIns_.GetComponent<EnemyStats>().SetID(_count);
        bonusMenu.GetEnemyIns(enemyIns_);
    }

    public void ChangeEnemy(int count_)
    {
        count = count_;
        if (allEnemyTab.Length > 0)
        {
            enemyForSpawn = allEnemyTab[count_];
        }
        GameManager.Instance.DisplayEnemyCount(count);
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

    private void AddOneEnemy(GameObject _enemyForSpawn)
    {
        if(_enemyForSpawn == allEnemyTab[0])
        {
            GameManager.Instance.enemyCountInGame_cac++;
            GameManager.Instance.DisplayEnemyCount(0);
        }
        else if(_enemyForSpawn == allEnemyTab[1])
        {
            GameManager.Instance.enemyCountInGame_dis++;
            GameManager.Instance.DisplayEnemyCount(1);
        }
        else
        {
            GameManager.Instance.enemyCountInGame_boss++;
            GameManager.Instance.DisplayEnemyCount(2);
        }
    }

    public bool CanSpawn()
    {
        if(enemyForSpawn == allEnemyTab[0])
        {
            return (GameManager.Instance.enemyCountInGame_cac < GameManager.Instance.enemyCountMax_cac);
        }
        else if(enemyForSpawn == allEnemyTab[1])
        {
            return (GameManager.Instance.enemyCountInGame_dis < GameManager.Instance.enemyCountMax_dis);
        }
        else
        {
            return (GameManager.Instance.enemyCountInGame_boss < GameManager.Instance.enemyCountMax_boss);
        }
    }
}
