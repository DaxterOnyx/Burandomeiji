using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI timer;

    private bool matchInProgress = true;
    private bool matchFinish = false;
    private bool canReduceTime = true;
    private int timeMatch;
    private int timeRemaining;

    private void Start()
    {
        timeMatch = GameManager.instance.matchSettings.time;
        timeRemaining = timeMatch;
        SetTimer();
    }

    private void Update()
    {
        if (matchInProgress && canReduceTime)
        {
            StartCoroutine(ReduceTime());
        }
    }

    private IEnumerator ReduceTime()
    {
        canReduceTime = false;
        if (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1;
            SetTimer();
        }
        else
        {
            matchInProgress = false;
            matchFinish = true;
        }
        canReduceTime = true;
    }

    public string GetTime()
    {
        int minute = timeRemaining / 60;
        int second = timeRemaining % 60;
        string time = minute + ":" + second.ToString("00");
        return time;
    }


    private void SetTimer()
    {
        timer.text = GetTime();
    }

}
