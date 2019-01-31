using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarVR : MonoBehaviour {

    [SerializeField] private RectTransform healthBarFill;

    public void SetHealthBar(float _currentAmount, float _healthMax)
    {
        healthBarFill.localScale = new Vector3(_currentAmount / _healthMax, 1f, 1f);
        if(_currentAmount <= 0f)
        {
            GameManager.Instance.SetBoolEnd(true);
        }
    }
}
