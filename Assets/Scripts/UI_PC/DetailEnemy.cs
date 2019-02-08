using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailEnemy : MonoBehaviour {

    private TextMeshProUGUI textDetail;

    [SerializeField] private GameObject playerPC;

    private PlayerPCSpawn playerPCSpawn;

    private int countEnemy;
	
    private void Start()
    {
        if(playerPC != null)
        {
            playerPCSpawn = playerPC.GetComponent<PlayerPCSpawn>();
            countEnemy = playerPCSpawn.GetCount();
            UpdateText(countEnemy);
        }
    }

    private void Update()
    {
        int tmp = playerPCSpawn.GetCount();
        if(tmp != countEnemy)
        {
            countEnemy = tmp;
            UpdateText(tmp);
        }
    }

    public void UpdateText(int _countEnemy)
    {

    }
}
