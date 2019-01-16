using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPCController))]
[RequireComponent(typeof(PlayerPCSpawn))]
public class PlayerPC : MonoBehaviour {

    /* Différents scripts */
    private PlayerPCController playerPCController;
    private PlayerPCSpawn playerPCSpawn;
    private UIPlayerPC UI;

    /* Prefabs et instance */
    [SerializeField] private GameObject UIPlayerPCPrefabs;
    private GameObject UIPlayerPCInstance;

    /* Attributs */
    private bool canSpawn = true;
    private float currentMana;
    [SerializeField] private float maxMana = 1000f;

	private void Awake ()
    {
        playerPCSpawn = GetComponent<PlayerPCSpawn>();
        playerPCController = GetComponent<PlayerPCController>();

        UIPlayerPCInstance = Instantiate(UIPlayerPCPrefabs);
        UIPlayerPCInstance.name = UIPlayerPCPrefabs.name;
        UI = UIPlayerPCInstance.GetComponent<UIPlayerPC>();

        currentMana = maxMana;
        UI.SetMana(maxMana, currentMana);
        UI.SetAllEnemyTab(playerPCSpawn.allEnemyTab);
	}
	
	private void Update ()
    {
        if(canSpawn)
        {
            Ray ray = new Ray(this.transform.position, this.transform.TransformDirection(Vector3.forward));
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Terrain")
                {
                    //Debug.DrawRay(hit.point, hit.normal * 10, Color.green);
                    if (playerPCController.ClickDown)
                    {
                        StartCoroutine(playerPCSpawn.Spawn(hit));
                    }
                }
            }
        }
        
        if(playerPCController.ScrollWheel < 0f)
        {
            UI.IconLeft();
        }
        else if(playerPCController.ScrollWheel > 0f)
        {
            UI.IconRight();
        }
    }

    IEnumerator RegenMana()
    {
        yield return new WaitForSeconds(1f);

    }
}
