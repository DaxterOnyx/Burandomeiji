using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusMenu : MonoBehaviour {

    private List<GameObject> bonusIconList = new List<GameObject>();
    [HideInInspector] public GameObject[] allEnemyTab;
    public List<int[]> multList;
    public List<int[]> costList;
    public List<int[]>[] bonusTab;

    [SerializeField] private TextMeshProUGUI hightLeftText;
    [SerializeField] private TextMeshProUGUI hightRightText;
    [SerializeField] private TextMeshProUGUI centerLeftText;
    [SerializeField] private TextMeshProUGUI centerRightText;
    [SerializeField] private TextMeshProUGUI lowLeftText;
    [SerializeField] private TextMeshProUGUI lowRightText;

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
        multList = new List<int[]>();
        costList = new List<int[]>();
        bonusTab = new List<int[]>[2];

        foreach(GameObject GO in allEnemyTab)
        {
            int[] tabM = new int[5];
            int[] tabC = new int[5];

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

    public TextMeshProUGUI SetManaCost(int _i, TextMeshProUGUI _manaCost)
    {
        EnemyStats stats = allEnemyTab[_i].GetComponent<EnemyStats>();
        int costMana = stats.mana;

        for(int j = 0; j < bonusTab[1][_i].Length; j++)
        {
            costMana += bonusTab[1][_i][j];
        }

        _manaCost.text = "-" + costMana + " mana";
        return _manaCost;
    }
}
