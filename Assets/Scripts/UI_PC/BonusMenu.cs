﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(EnemyMenu))]
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
    [SerializeField] private TextMeshProUGUI manaCost;

    [SerializeField] private RectTransform bonusBar;
    private List<GameObject> bonusIconList = new List<GameObject>();

    // 0:speed, 1:health, 2:critical, 3:hitDamage, 4:attackSpeed
    [SerializeField] private GameObject[] iconBonusTab;
    [SerializeField] private float[] costManaTab;
    [SerializeField] private float[] multTab;
    private int currentIcon;
    private int currentEnemyIcon;
    private float distance;

    private bool canDowngrade = true;

    EnemyMenu enemyMenu;

    private void Start()
    {
        enemyMenu = GetComponent<EnemyMenu>();
        distance = 1f;
    }

    private void Update()
    {
        UpdateManaCost(currentEnemyIcon, manaCost);
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

        float mult = bonusTab[0][currentEnemyIcon][currentIcon];

        switch (currentIcon)
        {
            case 0:
                lowLeftText.text = "Coût : ± " + costManaTab[currentIcon];
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Speed : " + (mult * 100).ToString("0") + " %";
                hightRightText.text = (enemyStats_.speedNoMult * mult).ToString("0.0") + " speed";
                break;

            case 1:
                lowLeftText.text = "Coût : ± " + costManaTab[currentIcon];
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Health : " + (mult * 100).ToString("0") + " %";
                hightRightText.text = (enemyStats_.healthNoMult * mult).ToString("0.0") + " health";
                break;

            case 2:
                lowLeftText.text = "Coût : ± " + costManaTab[currentIcon];
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Critical rate : " + (mult * 100).ToString("0") + " %";
                hightRightText.text = (enemyStats_.criticalNoMult * mult).ToString("0.0") + " % critical chance";
                break;

            case 3:
                lowLeftText.text = "Coût : ± " + costManaTab[currentIcon];
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Attack : " + (mult * 100).ToString("0") + " %";
                hightRightText.text = (enemyStats_.hitDamageNoMult * mult).ToString("0.0") + " damage";
                break;

            case 4:
                lowLeftText.text = "Coût : ± " + costManaTab[currentIcon];
                lowRightText.text = "Bonus : ± " + multTab[currentIcon]*100 + " %";
                centerRightText.text = " Attack speed : " + (mult * 100).ToString("0") + " %";
                hightRightText.text = (enemyStats_.hitDamageNoMult * mult).ToString("0.0") + " attack per second";
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
            float[] tabM_ = new float[5];
            float[] tabC_ = new float[5];

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
                stats_.multSpeed = bonusTab[0][i][0];
                stats_.multHealth = bonusTab[0][i][1];
                stats_.multCritical = bonusTab[0][i][2];
                stats_.multHitDamage = bonusTab[0][i][3];
                stats_.multHitCooldown = bonusTab[0][i][4];

                stats_.costSpeed = bonusTab[1][i][0];
                stats_.costHealth = bonusTab[1][i][1];
                stats_.costCritical = bonusTab[1][i][2];
                stats_.costHitDamage = bonusTab[1][i][3];
                stats_.costHitCooldown = bonusTab[1][i][4];
            }
        }
    }

    public TextMeshProUGUI UpdateManaCost(int _currentEnemyIcon, TextMeshProUGUI _manaCost)
    {
        manaCost = _manaCost;
        EnemyStats stats_ = allEnemyTab[_currentEnemyIcon].GetComponent<EnemyStats>();
        float costMana_ = stats_.mana;

        for(int j = 0; j < bonusTab[1][_currentEnemyIcon].Length; j++)
        {
            costMana_ += bonusTab[1][_currentEnemyIcon][j];
        }

        // FORMULE POUR MODIFIER LE COUT EN MANA PAR RAPPORT A LA DISTANCE DU JOEUR VR
        
        return _manaCost;
    }

    public void GetDistance(float _distance)
    {
        distance = _distance;
    }

    public float UpdateLostMana()
    {
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
