using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager> {

    public int enemyCountInGame = 0;

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
        if (!end)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0f;
                end = true;
                playerVRWin();
            }
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

    public void Win()
    {
        if(end)
        {
            if(timer == 0f)
            {
                playerVRWin();
            }
            else
            {
                playerPCWin();
            }
        }
    }

    private void playerPCWin()
    {
        Debug.Log("Le joueur PC a gagné !");
    }

    private void playerVRWin()
    {
        Debug.Log("Le joueur VR a gagné !");
    }

    public bool isGameFinish()
    {
        return end;
    }

    public void SetBoolEnd(bool _end)
    {
        end = _end;
        Win();
    }
}
