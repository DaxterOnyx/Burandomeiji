using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemyScript : MonoBehaviour {

    public GameObject[] enemyPrefabs;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject enemmi in enemyPrefabs)
        {
            Debug.Log(enemmi.name);
            Instantiate(enemmi, this.transform.position, this.transform.rotation);
        }
    }
}
