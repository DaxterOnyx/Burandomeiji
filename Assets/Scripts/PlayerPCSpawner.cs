﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPCController))]
[RequireComponent(typeof(PlayerPCMotor))]
public class PlayerPCSpawner : MonoBehaviour
{
    private PlayerPCController playerPCController;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject spawnerPrefabs;

    // Liste des ennemies qui seront spawn au prochain spawner
    [HideInInspector] public List<GameObject> enemyList = new List<GameObject>();
    // Liste des spawner sur la map
    private List<GameObject> spawnerList = new List<GameObject>();
    // Tableau contenant tout les ennemies
    public GameObject[] allEnemyTab;

    

    private void Start()
    {
        playerPCController = GetComponent<PlayerPCController>();
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
