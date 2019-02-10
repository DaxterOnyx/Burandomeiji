using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : SingletonBehaviour<GameManager> {

    [HideInInspector] public int enemyCountInGame_cac = 0;
    [HideInInspector] public int enemyCountInGame_dis = 0;
    [HideInInspector] public int enemyCountInGame_boss = 0;
    private int enemySelected;

    [HideInInspector] public int enemyCountMax_cac = 3;
    [HideInInspector] public int enemyCountMax_dis = 3;
    [HideInInspector] public int enemyCountMax_boss = 0;
    private bool canSetEnemyMax = true;

    [SerializeField] private float timer = 600f;
    [SerializeField] private float timerMatchBegin = 20f;

    private bool end = false;
    public bool matchIsProgress = false;


    public void Start()
    {
        end = false;
        matchIsProgress = false;
    }

    void Update()
    {
        if (!end && matchIsProgress)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0f;
                end = true;
                playerVRWin();
            }

            if(canSetEnemyMax)
                StartCoroutine(SetEnemyMax());
        }
        else
        {
            if(!matchIsProgress && !end)
            {
                if(timerMatchBegin > 0f)
                {
                    timerMatchBegin -= Time.deltaTime;
                }
                else
                {
                    matchIsProgress = true;
                }
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

    private IEnumerator SetEnemyMax()
    {
        canSetEnemyMax = false;

        enemyCountMax_cac = (int)(-0.045f * timer + 30f);
        enemyCountMax_dis = (int)(-0.0283f * timer + 20f);
        enemyCountMax_boss = (int)(-0.00833f * timer + 4f);
        DisplayEnemyCount(enemySelected);
        yield return new WaitForSeconds(10f);
        canSetEnemyMax = true;
    }


    public void DisplayEnemyCount(int _enemy)
    {
        enemySelected = _enemy;
        TextMeshProUGUI textCount = GameObject.FindGameObjectWithTag("CountEnemy").GetComponent<TextMeshProUGUI>();

        if(_enemy == 0)
        {
            textCount.text = enemyCountInGame_cac + " / " + enemyCountMax_cac;
        }
        else if(_enemy == 1)
        {
            textCount.text = enemyCountInGame_dis + " / " + enemyCountMax_dis;
        }
        else if(_enemy == 2)
        {
            textCount.text = enemyCountInGame_boss + " / " + enemyCountMax_boss;
        }   
    }
}
