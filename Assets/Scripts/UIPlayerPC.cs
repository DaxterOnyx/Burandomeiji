using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerPC : MonoBehaviour {

    [SerializeField] protected RectTransform manaBarFill;
    [SerializeField] protected RectTransform enemyBar;

    protected List<GameObject> enemyIconList = new List<GameObject>();
    [HideInInspector] public GameObject[] allEnemyTab;

    [HideInInspector] public int currentEnemyIcon;

    public void SetMana(float _manaMax, float _amount)
    {
        manaBarFill.localScale = new Vector3(_amount/_manaMax, 1f, 1f);
    }

    public void SetAllEnemyTab(GameObject[] tab_)
    {
        allEnemyTab = tab_;
        SetIcon();
    }

    private void SetIcon()
    {
        for (int i = 0; i < 3; i++)
        {
            InsIcon(i % allEnemyTab.Length, true);
            currentEnemyIcon = 1;
        }
    }

    protected void InsIcon(int number_, bool active_)
    {
        if (number_ >= 0 && number_ < allEnemyTab.Length)
        {
            GameObject Ins_ = Instantiate(allEnemyTab[number_].GetComponent<EnemyStats>().icon);
            enemyIconList.Add(Ins_);
            Ins_.transform.SetParent(enemyBar);
            Ins_.SetActive(active_);
            Ins_.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public int IconRight()
    {
        enemyIconList.ForEach(Destroy);
        enemyIconList.Clear();

        currentEnemyIcon = (currentEnemyIcon + 1) % allEnemyTab.Length;

        if ((currentEnemyIcon - 1) % allEnemyTab.Length == -1)
        {
            InsIcon(allEnemyTab.Length - 1, true);
        }
        else
        {
            InsIcon((currentEnemyIcon - 1) % allEnemyTab.Length, true);
        }

        InsIcon(currentEnemyIcon % allEnemyTab.Length, true);
        InsIcon((currentEnemyIcon + 1) % allEnemyTab.Length, true);
        return currentEnemyIcon;
    }

    public int IconLeft()
    {
        enemyIconList.ForEach(Destroy);
        enemyIconList.Clear();


        if ((currentEnemyIcon - 1) % allEnemyTab.Length == -1)
        {
            currentEnemyIcon = allEnemyTab.Length - 1;
        }
        else
        {
            currentEnemyIcon = (currentEnemyIcon - 1) % allEnemyTab.Length;
        }


        if ((currentEnemyIcon - 1) % allEnemyTab.Length == -1)
        {
            InsIcon(allEnemyTab.Length - 1, true);
        }
        else
        {
            InsIcon((currentEnemyIcon - 1) % allEnemyTab.Length, true);
        }

        InsIcon(currentEnemyIcon % allEnemyTab.Length, true);
        InsIcon((currentEnemyIcon + 1) % allEnemyTab.Length, true);
        return currentEnemyIcon;
    }
}
