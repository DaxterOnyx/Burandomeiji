using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBarScript : MonoBehaviour {

    [SerializeField] private RectTransform manaBarFill;
    [SerializeField] private TextMeshProUGUI textMana;
    [SerializeField] private TextMeshProUGUI textManaRegen;

    private float manaRegen;

    public float SetMana(float manaMax_, float amount_)
    {
        if (amount_ < 0)
        {
            amount_ = 0;
        }
        manaBarFill.localScale = new Vector3(amount_ / manaMax_, 1f, 1f);
        textMana.text = amount_.ToString("0") + " / " + manaMax_.ToString("0");

        manaRegen = manaMax_ *  ((600f - GameManager.Instance.timeRemain()) / 3300f) + 10f;
        manaRegen = Mathf.Clamp(manaRegen, 10f, manaMax_ * 0.3f);
        SetManaRegen(manaRegen);
        return manaRegen;
        //textMana.text = ((amount_/manaMax_)*100).ToString("0") + "%";
    }

    public void SetManaRegen(float _manaRegen)
    {
        manaRegen = _manaRegen;
        textManaRegen.text = "+" + manaRegen.ToString("0") + "            / sec";
        //textManaRegen.text = "+" + ((manaRegen/manaMax)*100).ToString("0") + "% mana / sec";
    }
}