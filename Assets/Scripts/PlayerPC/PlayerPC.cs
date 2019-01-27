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
    private GameObject cursorGO;
    private Transform target;

    /* Attributs */
    private bool canSpawn = true;
    private float currentMana;
    [SerializeField] private float maxMana = 1000f;
    [SerializeField] private float manaRegen = 550f;
    private float click;
    private bool canClick = true;

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
        cursorGO = GameObject.FindGameObjectWithTag("Cursor");
        imageCursor = cursorGO.GetComponent<Image>();
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

        if(canSpawn)
        {
            Ray ray = new Ray(this.transform.position, this.transform.TransformDirection(Vector3.forward));
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Terrain")
                {
                    float distanceVRCursor = Mathf.Abs(Vector3.Distance(hit.point, target.transform.position));
                    if (distanceVRCursor >= 15f)
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
            if (playerPCController.ScrollWheel > 0f)
            {
                bonusMenuScript.IconUp();
            }
            else if (playerPCController.ScrollWheel < 0f)
            {
                bonusMenuScript.IconDown();
            }

            if(playerPCController.Click0 || playerPCController.Click1)
            {
                if (canClick)
                {
                    StartCoroutine(TimeBetweenTwoClick());
                }
            }

            
            if(!playerPCController.Click0 && !playerPCController.Click1)
            {
                click = 0.3f;
            }

            if(playerPCController.A)
            {
                playerPCSpawn.ChangeEnemy(enemyMenuScript.IconLeft());
                bonusMenuScript.UpdateText();
            }

            if(playerPCController.E)
            {
                playerPCSpawn.ChangeEnemy(enemyMenuScript.IconRight());
                bonusMenuScript.UpdateText();
            }
        }
        
        if(playerPCController.BonusMenu)
        {
            canSpawn = !canSpawn;
            cursorGO.SetActive(canSpawn);
            bonusMenuGo.SetActive(!canSpawn);
            bonusMenuScript.UpdateText();
        }
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

    private IEnumerator TimeBetweenTwoClick()
    {
        canClick = false;
        if (playerPCController.Click0)
        {
            bonusMenuScript.UpgradeBonus();
        }    
        else if(playerPCController.Click1)
        {
            bonusMenuScript.DowngradeBonus();
        }

        if (click > 0.2f)
        {
            click -= 0.1f;
        }

        yield return new WaitForSeconds(click);
        canClick = true;
    }
}
