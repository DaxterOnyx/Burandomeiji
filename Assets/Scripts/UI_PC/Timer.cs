using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI timer;

    void Start()
    {
        timer.text = GameManager.Instance.getTimeString(GameManager.Instance.TimeDisplay());
    }

    void LateUpdate()
    {
        timer.text = GameManager.Instance.getTimeString(GameManager.Instance.TimeDisplay());
    }
}
