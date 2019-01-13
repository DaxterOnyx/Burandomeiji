using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPCController))]
[RequireComponent(typeof(PlayerPCMotor))]
public class PlayerPCSpawner : MonoBehaviour
{
    private PlayerPCController playerPCController;
    private Spawner spawnerScript;
    [SerializeField] private GameObject enemy;

    
    [HideInInspector] public List<GameObject> enemyList;
    private List<GameObject> spawnerList;

    [SerializeField] private GameObject spawnerPrefabs;

    private void Start()
    {
        playerPCController = GetComponent<PlayerPCController>();
        enemyList = new List<GameObject>();
        spawnerList = new List<GameObject>();
        enemyList.Add(enemy);
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
                    Debug.Log("Click");
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
    }

    public void DestroySpawner(GameObject _spawner)
    {
        spawnerList.Remove(_spawner);
        Destroy(_spawner, 2f);
    }
}
