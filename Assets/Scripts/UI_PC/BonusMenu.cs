using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(EnemyMenu))]
public class BonusMenu : MonoBehaviour {
  
    /// Ordre dans le tableau : 0:speed, 1:health, 2:critical, 3:hitDamage, 4:attackSpeed
    
    [HideInInspector] public GameObject[] allEnemyTab;  // Tableau contenant tout les types d'ennemies
    [SerializeField] private float[] costManaTabSup;    // Tableau des coûts en mana pour les upgrades > 100%
    [SerializeField] private float[] costManaTabInf;    // Tableau des coûts en mana pour les downgrades < 100%
    [SerializeField] private float[] multTab;   // Tableau des multiplicateurs
    public List<float[]> multList;  // Liste de tableau des multiplicateurs -> Chaque tableau de la liste correspond à un ennemi du tableau allEnemyTab
    public List<float[]> costList;  // Liste de tableau des coûts en mana de chaque bonus -> Chaque tableau de la liste correspond à un ennemi du tableau allEnemyTab
    public List<float[]>[] bonusTab;    // Tableau contenant les deux listes précédentes (multList & costList)

    /* Les différents textes du menu de bonus */
    [SerializeField] private TextMeshProUGUI hightLeftText;
    [SerializeField] private TextMeshProUGUI hightRightText;
    [SerializeField] private TextMeshProUGUI centerLeftText;
    [SerializeField] private TextMeshProUGUI centerRightText;
    [SerializeField] private TextMeshProUGUI lowLeftText;
    [SerializeField] private TextMeshProUGUI lowRightText;

    [SerializeField] private RectTransform bonusBar;    // Le transform de la bar contenant les icones des bonus
    private List<GameObject> bonusIconList = new List<GameObject>();    // Liste des icons de bonus acctuellement instanciés

    

    [SerializeField] private GameObject[] iconBonusTab; // Tableau des icons pour chaque bonus
    
    private int currentIcon;    // Indique quel bonus est actuellement selectionné par le joueur
    private int currentEnemyIcon;   // Indique quel ennemie est actuellement selectionné par le joueur

    private bool canUpgrade = true;
    private bool canDowngrade = true;

    EnemyMenu enemyMenu;

    private void Start()
    {
        enemyMenu = GetComponent<EnemyMenu>();
    }
    #region UI

    private void InsIcon(int _number)
    {
        if (_number >= 0 && _number < iconBonusTab.Length)
        {
            GameObject Ins_ = Instantiate(iconBonusTab[_number]);
            bonusIconList.Add(Ins_);
            Ins_.transform.SetParent(bonusBar);
            Ins_.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void SetIcon()
    {
        for (int i = 0; i < iconBonusTab.Length; i++)
        {
            InsIcon(i);
        }
        currentIcon = 2;
        UpdateText();
    }

    public void IconUp()
    {
        if ((currentIcon - 1) % iconBonusTab.Length == -1)
        {
            currentIcon = iconBonusTab.Length - 1;
        }
        else
        {
            currentIcon = (currentIcon - 1) % iconBonusTab.Length;
        }

        IconDeplace(currentIcon);
    }

    public void IconDown()
    {
        currentIcon = (currentIcon + 1) % iconBonusTab.Length;
        IconDeplace(currentIcon);  
    }

    private void IconDeplace(int _currentIcon)
    {
        bonusIconList.ForEach(Destroy);
        bonusIconList.Clear();

        if ((_currentIcon - 2) % iconBonusTab.Length == -1)
        {
            InsIcon(iconBonusTab.Length - 1);
        }
        else if ((_currentIcon - 2) % iconBonusTab.Length == -2)
        {
            InsIcon(iconBonusTab.Length - 2);
        }
        else
        {
            InsIcon((_currentIcon - 2) % iconBonusTab.Length);
        }

        if ((_currentIcon - 1) % iconBonusTab.Length == -1)
        {
            InsIcon(iconBonusTab.Length - 1);
        }
        else
        {
            InsIcon((_currentIcon - 1) % iconBonusTab.Length);
        }

        InsIcon((_currentIcon) % iconBonusTab.Length);
        InsIcon((_currentIcon + 1) % iconBonusTab.Length);
        InsIcon((_currentIcon + 2) % iconBonusTab.Length);
        UpdateText();
    }

    public void UpdateText()
    {
        currentEnemyIcon = GetComponent<EnemyMenu>().currentEnemyIcon;
        EnemyStats enemyStats_ = allEnemyTab[currentEnemyIcon].GetComponent<EnemyStats>();
        TakeHits takeHits_ = allEnemyTab[currentEnemyIcon].GetComponent<TakeHits>();
        DoHits doHits = allEnemyTab[currentEnemyIcon].GetComponentInChildren<DoHits>();

        hightLeftText.text = enemyStats_.enemyName;
        centerLeftText.text = "Type : " + enemyStats_.type;

        float mult = bonusTab[0][currentEnemyIcon][currentIcon];
        
        if(mult * 100 > 100f)
        {
            lowLeftText.text = "Coût : ± " + costManaTabSup[currentIcon];
        }
        else if(mult * 100 < 100f)
        {
            lowLeftText.text = "Coût : ± " + costManaTabInf[currentIcon];
        }
        else
        {
            lowLeftText.text = "Coût : +" + costManaTabSup[currentIcon] + "/-" + costManaTabInf[currentIcon];
        }

        switch (currentIcon)
        {
            case 0:       
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Speed : " + (mult * 100).ToString("0") + " %";
                hightRightText.text = (enemyStats_.speed * mult).ToString("0.0") + " speed";
                break;

            case 1:
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Health : " + (mult * 100).ToString("0") + " %";
                hightRightText.text = (takeHits_.health * mult).ToString("0.0") + " health";
                break;

            case 2:
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Critical rate : " + (mult * 100).ToString("0") + " %";
                hightRightText.text = (doHits.critical * mult).ToString("0.0") + " % critical chance";
                break;

            case 3:
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Attack : " + (mult * 100).ToString("0") + " %";
                hightRightText.text = (doHits.hitDamage * mult).ToString("0.0") + " damage";
                break;

            case 4:
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Attack speed : " + (mult * 100).ToString("0") + " %";
                hightRightText.text = (doHits.hitCooldown * mult).ToString("0.0") + " attack per second";
                break;

            default:
                Debug.LogError("Problème dans le switch du script BonusMenu!");
                break;
        }

    }

    /// Ordre dans le tableau : 0:speed, 1:health, 2:critical, 3:hitDamage, 4:attackSpeed
    public void UpgradeBonus()
    {
        canUpgrade = true;
        currentEnemyIcon = enemyMenu.currentEnemyIcon;

        SwitchBonus(true);
        
        if (canUpgrade)
        {
            if (bonusTab[0][currentEnemyIcon][currentIcon] * 100 > 100f)
            {
                bonusTab[1][currentEnemyIcon][currentIcon] += costManaTabSup[currentIcon]; 
            }
            else
            {
                bonusTab[1][currentEnemyIcon][currentIcon] += costManaTabInf[currentIcon];
            }         
        }

        enemyMenu.manaCost = UpdateManaCost(currentEnemyIcon, enemyMenu.manaCost);
        UpdateText();
        
    }

    public void DowngradeBonus()
    {
        currentEnemyIcon = enemyMenu.currentEnemyIcon;
        canDowngrade = true;

        SwitchBonus(false);

        if (canDowngrade)
        {
            if (bonusTab[0][currentEnemyIcon][currentIcon] * 100 < 100f)
            {
                bonusTab[1][currentEnemyIcon][currentIcon] -= costManaTabInf[currentIcon];
            }
            else
            {
                bonusTab[1][currentEnemyIcon][currentIcon] -= costManaTabSup[currentIcon];
            }
        }
        enemyMenu.manaCost = UpdateManaCost(currentEnemyIcon, enemyMenu.manaCost);
        UpdateText();
    }

    private void SwitchBonus(bool _switch)
    {
        switch (currentIcon)
        {
            case 0:
                CapBonus(0.25f, 2.05f, _switch);
                break;
            case 1:
                CapBonus(0.55f, 3.25f, _switch);
                break;
            case 2:
                CapBonus(0.2f, 3f, _switch);
                break;
            case 3:
                CapBonus(0.5f, 2.5f, _switch);
                break;
            case 4:
                CapBonus(0.5f, 2f, _switch);
                break;
            default:
                Debug.LogError("currentIcon error!");
                break;
        }
    }

    private void CapBonus(float _min, float _max, bool _switch)
    {
        if(_switch)
        {
            bonusTab[0][currentEnemyIcon][currentIcon] = Mathf.Clamp(bonusTab[0][currentEnemyIcon][currentIcon] + multTab[currentIcon], _min, _max);
            if (bonusTab[0][currentEnemyIcon][currentIcon] == _max)
            {
                canUpgrade = false;
            }
        }
        else
        {
            bonusTab[0][currentEnemyIcon][currentIcon] = Mathf.Clamp(bonusTab[0][currentEnemyIcon][currentIcon] - multTab[currentIcon], _min, _max);
            if (bonusTab[0][currentEnemyIcon][currentIcon] == _min)
            {
                canDowngrade = false;
            }
        }
    }

    

    #endregion

    #region Bonus

    public void SetAllEnemyTab(GameObject[] tab_)
    {
        allEnemyTab = tab_;
        SetBonusTab();
    }

    /* 0/1 : mult ou cost
     * 0/x : ID du monstre
     * 0/4 : selection du mult ou cost -> 0:speed, 1:health, 2:critical, 3:hitDamage, 4:attackSpeed */

    private void SetBonusTab()
    {
        multList = new List<float[]>();
        costList = new List<float[]>();
        bonusTab = new List<float[]>[2];

        foreach(GameObject GO in allEnemyTab)
        {
            float[] tabM_ = new float[5];   // Tableau des multiplicateurs
            float[] tabC_ = new float[5];   // Tableau des coûts en mana

            for(int i = 0; i < tabM_.Length; i++)
            {
                tabM_[i] = 1;
                tabC_[i] = 0;
            }
            multList.Add(tabM_);
            costList.Add(tabC_);
        }
        bonusTab[0] = multList;
        bonusTab[1] = costList;
        SetIcon();
    }

    public void GetEnemyIns(GameObject _enemy)
    {     
        EnemyStats stats_ = _enemy.GetComponent<EnemyStats>();
        for(int i = 0; i < allEnemyTab.Length; i++)
        {
            if(stats_.ID == i)
            {
                stats_.SetStats(bonusTab[0][i][0], bonusTab[0][i][1], bonusTab[0][i][2], bonusTab[0][i][3], bonusTab[0][i][4]);
                stats_.SetCost(bonusTab[1][i][0] + bonusTab[1][i][1] + bonusTab[1][i][2] + bonusTab[1][i][3] + bonusTab[1][i][4]);
            }
        }
    }

    public TextMeshProUGUI UpdateManaCost(int _currentEnemyIcon, TextMeshProUGUI _costMana)
    {
        EnemyStats stats_ = allEnemyTab[_currentEnemyIcon].GetComponent<EnemyStats>();
        float costMana_ = stats_.mana;

        for(int j = 0; j < bonusTab[1][_currentEnemyIcon].Length; j++)
        {
            costMana_ += bonusTab[1][_currentEnemyIcon][j];
        }

        _costMana.text = "" + costMana_;
        
        return _costMana;
    }

    public float UpdateLostMana()
    {
        currentEnemyIcon = GetComponent<EnemyMenu>().currentEnemyIcon;
        EnemyStats enemyStats_ = allEnemyTab[currentEnemyIcon].GetComponent<EnemyStats>();

        float manaLost_ = enemyStats_.mana;

        for(int i = 0; i < bonusTab[1][currentEnemyIcon].Length; i++)
        {
            manaLost_ += bonusTab[1][currentEnemyIcon][i];
        }
        return manaLost_;
    }

    #endregion
}
