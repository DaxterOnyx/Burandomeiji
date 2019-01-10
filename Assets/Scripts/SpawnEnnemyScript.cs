using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemyScript : MonoBehaviour {

    [SerializeField]
    private List<GameObject> enemyPrefabs;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnEnnemy());
    }

    IEnumerator SpawnEnnemy()
    {
        foreach (GameObject enemmi in enemyPrefabs)
        {
            Debug.Log(enemmi.name);
            Instantiate(enemmi, this.transform.position, this.transform.rotation);
            yield return new WaitForSeconds(2f);
        }
    }
}
