using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour {

    [SerializeField] private RectTransform healthBarFill;
    [SerializeField] private TextMeshProUGUI textHealth;

    public void SetMana(float healthMax_, float currentAmount_)
    {
        if (currentAmount_ < 0)
        {
            currentAmount_ = 0;
        }
        healthBarFill.localScale = new Vector3(currentAmount_ / healthMax_, 1f, 1f);
        textHealth.text = ((currentAmount_/healthMax_)*100).ToString("0") + "%";
    }
}
