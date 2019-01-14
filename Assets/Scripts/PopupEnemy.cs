using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupEnemy : MonoBehaviour {

    [SerializeField] private PlayerPCController playerPCController;
    [SerializeField] private PlayerPCSpawner playerPCSpawner;

    private List<GameObject> popList = new List<GameObject>();

    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject enemy;

    private int count = 0;

	void Update ()
    {
		if(playerPCController.SwitchEnemy)
        {
            if (playerPCSpawner.allEnemyTab.Length > 0)
            {
                if(popList.Count > 0)
                {
                    foreach(GameObject pop in popList)
                    {
                        Destroy(pop);
                    }
                }

                count = (count + 1) % playerPCSpawner.allEnemyTab.Length;
                GameObject popIns_ = (GameObject)Instantiate(popup, this.transform);
                string enemyName = playerPCSpawner.allEnemyTab[count].GetComponent<IAScript>().enemyName;
                popIns_.GetComponent<PopupEnemyItem>().Setup(enemyName);
                popList.Add(popIns_);
            }
        }
	}
}
