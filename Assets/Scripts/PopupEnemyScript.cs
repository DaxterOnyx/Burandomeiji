using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupEnemyScript : MonoBehaviour {
    /*[SerializeField] private PlayerPCController playerPCController;

    public List<GameObject> enemyList = new List<GameObject>();
    private List<GameObject> popList = new List<GameObject>();

    [SerializeField] private GameObject popup;

    private int count = 0;

	private void Update ()
    {
        if(playerPCController.SwitchEnemy)
        {
            if (enemyList.Count > 0)
            {
                if(popList.Count > 0)
                {
                    foreach(GameObject pop in popList)
                    {
                        Destroy(pop);
                    }
                }

                count = (count + 1) % enemyList.Count;
                GameObject popIns_ = (GameObject)Instantiate(popup, this.transform);
                string enemyName = enemyList[count].GetComponent<IAScript>().enemyName;
                popIns_.GetComponent<PopupEnemyItem>().Setup(enemyName);
                popList.Add(popIns_);
            }
        }
	}*/
}
