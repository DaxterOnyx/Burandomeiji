using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPCController))]
[RequireComponent(typeof(PlayerPCMotor))]
public class PlayerPCSpawner : MonoBehaviour
{
    #region Avec plusieurs monstres
    /*private PlayerPCController playerPCController;
    [SerializeField] private GameObject spawnerPrefabs;

    // Liste des ennemies qui seront spawn au prochain spawner
    [HideInInspector] public List<GameObject> enemyList;
    // Liste des spawner sur la map
    private List<GameObject> spawnerList;
    // Tableau contenant tout les ennemies
    public GameObject[] allEnemyTab;

    private void Start()
    {
        enemyList = new List<GameObject>();
        spawnerList = new List<GameObject>();
        playerPCController = GetComponent<PlayerPCController>();
        foreach(GameObject enemy in allEnemyTab)
        {
            enemyList.Add(enemy);
        }
    }

    private void Update()
    {
        ///Click spawn
        Ray ray = new Ray(this.transform.position, this.transform.TransformDirection(Vector3.forward));
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Terrain")
            {
                Debug.DrawRay(hit.point, hit.normal * 10, Color.green);
                if (playerPCController.ClickDown) 
                {
                    if (enemyList.Count > 0)
                    {
                        SpawnSpawner(hit);
                    }
                }
            }
        }
    }

    public void SpawnSpawner(RaycastHit _hit)
    {
        GameObject spawnerIns_ = Instantiate(spawnerPrefabs, _hit.point, Quaternion.identity);
        spawnerList.Add(spawnerIns_);
        Spawner spawner = spawnerIns_.GetComponent<Spawner>();
        spawner.AddEnemyToList(enemyList);
        StartCoroutine(spawner.SpawnEnemy());
    }

    public void DestroySpawner(GameObject _spawner)
    {
        spawnerList.Remove(_spawner);
        Destroy(_spawner, 2f);
    }*/
    #endregion

    #region Avec un seul monstre

    private PlayerPCController playerPCController;
    [SerializeField] private GameObject spawnerPrefabs;

    private GameObject enemyForSpawn;
    
    // Tableau contenant tout les ennemies
    public GameObject[] allEnemyTab
    {
        get { return allEnemyTab; }
        private set { allEnemyTab = value; }
    }

    private void Start()
    {
        playerPCController = GetComponent<PlayerPCController>();
        enemyForSpawn = allEnemyTab[0];
    }

    private void Update()
    {
        ///Click spawn
        Debug.Log("enemy in Update : " + enemyForSpawn.name);
        Ray ray = new Ray(this.transform.position, this.transform.TransformDirection(Vector3.forward));
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Terrain")
            {
                Debug.DrawRay(hit.point, hit.normal * 10, Color.green);
                if (playerPCController.ClickDown) 
                {
                    if (enemyForSpawn != null)
                    {
                        StartCoroutine(Spawn(hit));
                    }
                }
            }
        }
    }

    private IEnumerator Spawn(RaycastHit _hit)
    {
        GameObject spawnerIns_ = Instantiate(spawnerPrefabs, _hit.point, Quaternion.identity);

        yield return new WaitForSeconds(0.6f);

        GameObject enemyIns_ = Instantiate(enemyForSpawn, spawnerIns_.transform.position, spawnerIns_.transform.rotation);

        Destroy(spawnerIns_, 1.5f);
    }

    public void ChangeEnemy(int count_)
    {
        enemyForSpawn = allEnemyTab[count_];
    }

    #endregion
}
