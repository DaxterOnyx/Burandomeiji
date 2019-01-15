using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupEnemy : MonoBehaviour {

    [SerializeField] private PlayerPCController playerPCController;
    [SerializeField] private PlayerPCSpawner playerPCSpawner;

    private List<GameObject> popList = new List<GameObject>();

    [SerializeField] private GameObject popup;

    private int count = 0;

	void Update ()
    {
		if(playerPCController.SwitchEnemy)
        {
            if (playerPCSpawner.allEnemyTab.Length > 0)
            {
                if(popList.Count > 0)
                {
                    GameObject pop = popList[0];
                    popList.Remove(pop);
                    Destroy(pop);
                    count = (count + 1) % playerPCSpawner.allEnemyTab.Length;
                }
                playerPCSpawner.ChangeEnemy(count);
                GameObject popIns_ = (GameObject)Instantiate(popup, this.transform);
                string enemyName = playerPCSpawner.allEnemyTab[count].GetComponent<EnemyStats>().enemyName;
                popIns_.GetComponent<PopupEnemyItem>().Setup(enemyName);
                Debug.Log(enemyName);
               
                popList.Add(popIns_);
                StartCoroutine(DeletePopup(popIns_));
            }
        }
	}

    IEnumerator DeletePopup(GameObject _popIns)
    {
        yield return new WaitForSeconds(3f);
        popList.Remove(_popIns);
        Destroy(_popIns);
    }
}
