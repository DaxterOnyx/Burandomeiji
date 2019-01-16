using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerPC : MonoBehaviour {

    [SerializeField] private RectTransform manaBarFill;

    public void SetMana(float _manaMax, float _amount)
    {
        manaBarFill.localScale = new Vector3(1f, _amount/_manaMax, 1f);
    }
}
