using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPCSpawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnerPrefabs;
    [HideInInspector] public GameObject enemyForSpawn;
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
        GameObject spawnerIns_ = Instantiate(spawnerPrefabs, _hit.point, Quaternion.identity);
        yield return new WaitForSeconds(0.6f);
        GameObject enemyIns_ = Instantiate(_enemyForSpawn, spawnerIns_.transform.position, spawnerIns_.transform.rotation);
        enemyIns_.GetComponent<EnemyStats>().GetID(_count);
        bonusMenu.GetEnemyIns(enemyIns_);
        Destroy(spawnerIns_, 1.5f);
    }

    public void ChangeEnemy(int count_)
    {
        count = count_;
        if (allEnemyTab.Length > 0)
        {
            enemyForSpawn = allEnemyTab[count_];
        }
    }

    public void GetUI(GameObject _UI)
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
