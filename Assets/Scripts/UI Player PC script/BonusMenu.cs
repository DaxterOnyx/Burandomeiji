using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusMenu : MonoBehaviour {
  
    [HideInInspector] public GameObject[] allEnemyTab;
    public List<float[]> multList;
    public List<float[]> costList;
    public List<float[]>[] bonusTab;

    [SerializeField] private TextMeshProUGUI hightLeftText;
    [SerializeField] private TextMeshProUGUI hightRightText;
    [SerializeField] private TextMeshProUGUI centerLeftText;
    [SerializeField] private TextMeshProUGUI centerRightText;
    [SerializeField] private TextMeshProUGUI lowLeftText;
    [SerializeField] private TextMeshProUGUI lowRightText;

    [SerializeField] private RectTransform bonusBar;
    private List<GameObject> bonusIconList = new List<GameObject>();

    // 0:speed, 1:health, 2:critical, 3:hitDamage, 4:attackSpeed
    [SerializeField] private GameObject[] iconBonusTab;
    [SerializeField] private float[] costManaTab;
    [SerializeField] private float[] multTab;
    private int currentIcon;
    private int currentEnemyIcon;

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
        GameObject enemyGO_ = allEnemyTab[currentEnemyIcon];
        EnemyStats enemyStats_ = enemyGO_.GetComponent<EnemyStats>();

        hightLeftText.text = enemyStats_.enemyName;
        centerLeftText.text = "Type : " + enemyStats_.type;

        switch(currentIcon)
        {
            case 0:
                lowLeftText.text = "Coût : ± " + costManaTab[currentIcon] + " mana";
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Speed : " + (bonusTab[0][currentEnemyIcon][currentIcon]*100).ToString("0") + " %";
                hightRightText.text = enemyStats_.speed + " speed";
                break;

            case 1:
                lowLeftText.text = "Coût : ± " + costManaTab[currentIcon] + " mana";
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Health : " + (bonusTab[0][currentEnemyIcon][currentIcon] * 100).ToString("0") + " %";
                hightRightText.text = enemyStats_.health + " health";
                break;

            case 2:
                lowLeftText.text = "Coût : ± " + costManaTab[currentIcon] + " mana";
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Critical rate : " + (bonusTab[0][currentEnemyIcon][currentIcon] * 100).ToString("0") + " %";
                hightRightText.text = enemyStats_.critical + " % critical chance";
                break;

            case 3:
                lowLeftText.text = "Coût : ± " + costManaTab[currentIcon] + " mana";
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Attack : " + (bonusTab[0][currentEnemyIcon][currentIcon] * 100).ToString("0") + " %";
                hightRightText.text = enemyStats_.hitDamage + " damage";
                break;

            case 4:
                lowLeftText.text = "Coût : ± " + costManaTab[currentIcon] + " mana";
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Attack speed : " + (bonusTab[0][currentEnemyIcon][currentIcon] * 100).ToString("0") + " %";
                hightRightText.text = enemyStats_.hitDamage + " attack per second";
                break;

            default:
                Debug.LogError("Problème dans le switch du script BonusMenu!");
                break;
        }

    }

    public void UpgradeBonus()
    {
        currentEnemyIcon = enemyMenu.currentEnemyIcon;
        bonusTab[0][currentEnemyIcon][currentIcon] += multTab[currentIcon];
        bonusTab[1][currentEnemyIcon][currentIcon] += costManaTab[currentIcon];
        UpdateText();
        enemyMenu.manaCost = UpdateManaCost(currentEnemyIcon, enemyMenu.manaCost);
    }

    public void DowngradeBonus()
    {
        if (canDowngrade)
        {
            currentEnemyIcon = enemyMenu.currentEnemyIcon;
            bonusTab[0][currentEnemyIcon][currentIcon] -= multTab[currentIcon];



            bonusTab[1][currentEnemyIcon][currentIcon] -= costManaTab[currentIcon];
            UpdateText();
            enemyMenu.manaCost = UpdateManaCost(currentEnemyIcon, enemyMenu.manaCost);
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
            float[] tabM = new float[5];
            float[] tabC = new float[5];

            for(int i = 0; i < tabM.Length; i++)
            {
                tabM[i] = 1;
                tabC[i] = 0;
            }
            multList.Add(tabM);
            costList.Add(tabC);
        }
        bonusTab[0] = multList;
        bonusTab[1] = costList;
        SetIcon();
    }

    public void GetEnemyIns(GameObject _enemy)
    {     
        EnemyStats stats = _enemy.GetComponent<EnemyStats>();
        for(int i = 0; i < allEnemyTab.Length; i++)
        {
            if(stats.ID == i)
            {
                stats.multSpeed = bonusTab[0][i][0];
                stats.multHealth = bonusTab[0][i][1];
                stats.multCritical = bonusTab[0][i][2];
                stats.multHitDamage = bonusTab[0][i][3];
                stats.multHitCooldown = bonusTab[0][i][4];

                stats.costSpeed = bonusTab[1][i][0];
                stats.costHealth = bonusTab[1][i][1];
                stats.costCritical = bonusTab[1][i][2];
                stats.costHitDamage = bonusTab[1][i][3];
                stats.costHitCooldown = bonusTab[1][i][4];
            }
        }
    }

    public TextMeshProUGUI UpdateManaCost(int _currentEnemyIcon, TextMeshProUGUI _manaCost)
    {
        EnemyStats stats = allEnemyTab[_currentEnemyIcon].GetComponent<EnemyStats>();
        float costMana = stats.mana;

        for(int j = 0; j < bonusTab[1][_currentEnemyIcon].Length; j++)
        {
            costMana += bonusTab[1][_currentEnemyIcon][j];
        }

        /*if(costMana < stats.mana / 2)
        {
            costMana = stats.mana / 2;
            canDowngrade = false;
        }
        else
        {
            canDowngrade = true;
        }*/

        _manaCost.text = "-" + costMana + " mana";
        return _manaCost;
    }

    public float UpdateLostMana()
    {
        EnemyStats enemyStats_ = allEnemyTab[currentEnemyIcon].GetComponent<EnemyStats>();

        float manaLost = enemyStats_.mana;

        for(int i = 0; i < bonusTab[1][currentEnemyIcon].Length; i++)
        {
            manaLost += bonusTab[1][currentEnemyIcon][i];
        }

        if (manaLost < enemyStats_.mana / 2)
        {
            manaLost = enemyStats_.mana / 2;
        }

        return manaLost;
    }

    #endregion
}
