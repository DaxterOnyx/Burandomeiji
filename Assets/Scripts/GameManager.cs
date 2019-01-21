using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager> {

    [SerializeField]
    private float timer = 600;

    [SerializeField]
    private bool end = false;

    public void Start()
    {
        end = false;
    }

    void Update()
    {
        if(timer >0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            end = true;
        }
    }

    public string getTimeString()
    {
        int minute = (int)timer / 60;
        int second = (int)timer % 60;
        string time = minute + ":" + second.ToString("00");
        return time;
    }

    public float timeRemain()
    {
        return timer;
    }

    public bool isGameFinish()
    {
        return end;
    }
}
