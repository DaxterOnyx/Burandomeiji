using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerPCController))]
[RequireComponent(typeof(PlayerPCSpawn))]
public class PlayerPC : MonoBehaviour {

    /* Différents scripts */
        // scripts du player
    private PlayerPCController playerPCController;
    private PlayerPCSpawn playerPCSpawn;
        // scripts du UI
    private BonusMenu bonusMenuScript;
    private ManaBarScript manaBarScript;
    private EnemyMenu enemyMenuScript;

    /* Prefabs et instance */
    [SerializeField] private GameObject UIPlayerPCPrefabs;
    private GameObject UIPlayerPCInstance;
    private GameObject bonusMenuGo;
    private Transform target;

    /* Attributs */
    private float currentMana;
    [SerializeField] private float maxMana = 1000f;
    [SerializeField] private float manaRegen = 550f;

    Image imageCursor;

	private void Start ()
    {
        currentMana = maxMana;

        playerPCSpawn = GetComponent<PlayerPCSpawn>();
        playerPCController = GetComponent<PlayerPCController>();

        UIPlayerPCInstance = Instantiate(UIPlayerPCPrefabs);
        UIPlayerPCInstance.name = UIPlayerPCPrefabs.name;

        bonusMenuScript = UIPlayerPCInstance.GetComponent<BonusMenu>();
        enemyMenuScript = UIPlayerPCInstance.GetComponent<EnemyMenu>();
        manaBarScript = UIPlayerPCInstance.GetComponent<ManaBarScript>();

        playerPCSpawn.SetUI(UIPlayerPCInstance);
        enemyMenuScript.SetAllEnemyTab(playerPCSpawn.allEnemyTab);
        bonusMenuScript.SetAllEnemyTab(playerPCSpawn.allEnemyTab);
        manaBarScript.SetMana(maxMana, currentMana);
        manaBarScript.SetManaRegen(manaRegen);

        bonusMenuGo = GameObject.FindGameObjectWithTag("BonusMenu");
        imageCursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Image>();
        bonusMenuGo.SetActive(false);
    }
	
	private void Update ()
    {
        maxMana += Time.deltaTime/1.2f;

        if (currentMana < maxMana)
        {
            RegenMana();
        }

        if(target == null)
        {
            GameObject JoueurVR = GameObject.FindGameObjectWithTag("Player");
            if(JoueurVR != null)
            {
                target = JoueurVR.transform;
            }
            else
            {
                return;
            }
        }

        Spawn();
        Controller();
    }

    private void RegenMana()
    {
        currentMana = currentMana + manaRegen * Time.deltaTime;

        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        manaRegen = manaBarScript.SetMana(maxMana, currentMana);
    }

    private void Spawn()
    {
        Ray ray = new Ray(this.transform.position, this.transform.TransformDirection(Vector3.forward));
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Terrain")
            {
                float distanceVRCursor = Mathf.Abs(Vector3.Distance(hit.point, target.transform.position));

                if (distanceVRCursor >= 20f)
                {
                    if (playerPCController.ClickDown)
                    {
                        float lostMana = bonusMenuScript.UpdateLostMana();
                        if (currentMana >= lostMana)
                        {
                            StartCoroutine(playerPCSpawn.Spawn(hit, playerPCSpawn.GetEnemySpawn(), playerPCSpawn.GetCount()));
                            currentMana -= lostMana;
                        }
                    }
                    imageCursor.color = Color.HSVToRGB(0f, 0f, 0f);   // Noir
                }
                else
                {
                    imageCursor.color = Color.HSVToRGB(0f, 100f, 85f);   // Rouge
                }
            }
        }  
    }

    private void Controller()
    {
        // Si le joueur appuie sur la touche "BonusMenu"
        if (playerPCController.BonusMenu)
        {
            bonusMenuGo.SetActive(!bonusMenuGo.activeSelf);
            bonusMenuScript.UpdateText();
        }

        // Si le menu des bonus est désactivé
        if(bonusMenuGo.activeSelf == false)
        {
            if (playerPCController.ScrollWheel < 0f)
            {
                playerPCSpawn.ChangeEnemy(enemyMenuScript.IconLeft());
            }
            else if (playerPCController.ScrollWheel > 0f)
            {
                playerPCSpawn.ChangeEnemy(enemyMenuScript.IconRight());
            }
        }
        else // Si le menu des bonus est activé
        {
            if (playerPCController.ScrollWheel > 0f)
            {
                bonusMenuScript.IconUp();
            }
            else if (playerPCController.ScrollWheel < 0f)
            {
                bonusMenuScript.IconDown();
            }

            if (playerPCController.E)
            {
                bonusMenuScript.UpgradeBonus();
            }
            else if (playerPCController.A)
            {
                bonusMenuScript.DowngradeBonus();
            }
        } 
    }
}
