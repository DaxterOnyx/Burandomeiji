using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonusMenu : MonoBehaviour {

    private List<GameObject> bonusIconList = new List<GameObject>();
    [HideInInspector] public GameObject[] allEnemyTab;

    [SerializeField] private TextMeshProUGUI hightLeftText;
    [SerializeField] private TextMeshProUGUI hightRightText;
    [SerializeField] private TextMeshProUGUI centerLeftText;
    [SerializeField] private TextMeshProUGUI centerRightText;
    [SerializeField] private TextMeshProUGUI lowLeftText;
    [SerializeField] private TextMeshProUGUI lowRightText;

    public void SetAllEnemyTab(GameObject[] tab_)
    {
        allEnemyTab = tab_;
    }

}
