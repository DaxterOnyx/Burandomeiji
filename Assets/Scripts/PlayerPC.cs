using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPCController))]
[RequireComponent(typeof(PlayerPCSpawn))]
public class PlayerPC : MonoBehaviour {

    /* Différents scripts */
    private PlayerPCController playerPCController;
    private PlayerPCSpawn playerPCSpawn;
    private BonusMenu bonusMenuScript;
    private ManaBarScript manaBarScript;
    private EnemyMenu enemyMenuScript;

    /* Prefabs et instance */
    [SerializeField] private GameObject UIPlayerPCPrefabs;
    private GameObject UIPlayerPCInstance;
    private GameObject bonusMenuGo;
    private GameObject cursorGO;

    /* Attributs */
    private bool canSpawn = true;
    private float currentMana;
    [SerializeField] private float maxMana = 1000f;
    [SerializeField] private float manaRegen = 50f;

	private void Start ()
    {
        playerPCSpawn = GetComponent<PlayerPCSpawn>();
        playerPCController = GetComponent<PlayerPCController>();

        UIPlayerPCInstance = Instantiate(UIPlayerPCPrefabs);
        UIPlayerPCInstance.name = UIPlayerPCPrefabs.name;
        bonusMenuScript = UIPlayerPCInstance.GetComponent<BonusMenu>();
        enemyMenuScript = UIPlayerPCInstance.GetComponent<EnemyMenu>();
        manaBarScript = UIPlayerPCInstance.GetComponent<ManaBarScript>();
        playerPCSpawn.GetUI(UIPlayerPCInstance);

        currentMana = maxMana;
        
        enemyMenuScript.SetAllEnemyTab(playerPCSpawn.allEnemyTab);
        bonusMenuScript.SetAllEnemyTab(playerPCSpawn.allEnemyTab);
        manaBarScript.SetMana(maxMana, currentMana);
        manaBarScript.SetManaRegen(manaRegen);
        bonusMenuGo = GameObject.FindGameObjectWithTag("BonusMenu");
        cursorGO = GameObject.FindGameObjectWithTag("Cursor");
        bonusMenuGo.SetActive(false);
    }
	
	private void Update ()
    {
        if (currentMana < maxMana)
        {
            RegenMana();
        }

        if(canSpawn)
        {
            Ray ray = new Ray(this.transform.position, this.transform.TransformDirection(Vector3.forward));
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Terrain")
                {
                    //Debug.DrawRay(hit.point, hit.normal * 10, Color.green);
                    if (playerPCController.ClickDown0)
                    {
                        if(currentMana >= playerPCSpawn.enemyForSpawn.GetComponent<EnemyStats>().mana)
                        {
                            StartCoroutine(playerPCSpawn.Spawn(hit, playerPCSpawn.GetEnemySpawn(), playerPCSpawn.GetCount()));
                            currentMana -= playerPCSpawn.enemyForSpawn.GetComponent<EnemyStats>().mana;
                        }
                        
                    }
                }
            }
            if (playerPCController.ScrollWheel < 0f)
            {
                playerPCSpawn.ChangeEnemy(enemyMenuScript.IconLeft());
            }
            else if(playerPCController.ScrollWheel > 0f)
            {
                playerPCSpawn.ChangeEnemy(enemyMenuScript.IconRight());
            }
        }
        else
        {
            if (playerPCController.ScrollWheel < 0f)
            {
                // Tourner la roue des bonus vers le haut
            }
            else if (playerPCController.ScrollWheel > 0f)
            {
                // Tourner la roue des bonus vers le bas
            }

            if(playerPCController.ClickDown0)
            {
                // Augmenter le bonus
            }

            if (playerPCController.ClickDown1)
            {
                // Diminuer le bonus
            }
        }
        
        if(playerPCController.BonusMenu)
        {
            canSpawn = !canSpawn;
            cursorGO.SetActive(canSpawn);
            bonusMenuGo.SetActive(!canSpawn);
        }
    }

    private void RegenMana()
    {
        currentMana = currentMana + manaRegen * Time.deltaTime;

        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        manaBarScript.SetMana(maxMana, currentMana);
    }
}
