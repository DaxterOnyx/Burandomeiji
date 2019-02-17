using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : SingletonBehaviour<GameManager> {

    [SerializeField] private float timer = 420f;
    [SerializeField] private float timerMatchBegin = 20f;
    [SerializeField] private float timerMatchEnd = 20f;
    //public int pointSpawnMax = 60;
    private float timeDisplay;
    //public int pointSpawnCurrent;

    //public int pointSpawnPriceCAC;
    //public int pointSpawnPriceDIS;
    //public int pointSpawnPriceBOSS;
    private bool win = false;


    private bool end = false;
    public bool matchIsProgress = false;


    public void Start()
    {
        end = false;
        matchIsProgress = false;
        //pointSpawnCurrent = pointSpawnMax;
    }

    void Update()
    {
        if(timerMatchBegin > 0)
        {
            timeDisplay = timerMatchBegin;
            timerMatchBegin = UpdateTimer(timerMatchBegin);
        }
        else if(timer > 0 && !end)
        {
            matchIsProgress = true;
            timeDisplay = timer;
            timer = UpdateTimer(timer);
        }
        else if(timerMatchEnd > 0)
        {
            Win();  // Activé si le joueur VR gagne
            end = true;
            matchIsProgress = false;
            timeDisplay = timerMatchEnd;
            timerMatchEnd = UpdateTimer(timerMatchEnd);
            
        }
    }

    private float UpdateTimer(float _timer)
    {
        _timer -= Time.deltaTime;
        return _timer;
    }

    public string getTimeString(float _timer)
    {
        int minute_ = (int)_timer / 60;
        int second_ = (int)_timer % 60;
        string time_ = minute_ + ":" + second_.ToString("00");
        return time_;
    }

    public float TimeRemain()
    {
        return timer;
    }

    public float TimeRemainEnd()
    {
        return timerMatchEnd;
    }

    public float TimeRemainBegin()
    {
        return timerMatchBegin;
    }

    public float TimeDisplay()
    {
        return timeDisplay;
    }

    public void Win()
    {  
        if(end && !win)
        {
            matchIsProgress = false;
            win = true;
            GameObject[] enemyTab = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemy in enemyTab)
            {
                TakeHits takeHits = enemy.GetComponent<TakeHits>();
                takeHits.Die();
            }

            if (timer < 0f)
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
        StartCoroutine(EndGame());
    }

    private void playerVRWin()
    {
        Debug.Log("Le joueur VR a gagné !");
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }

    public bool isGameFinish()
    {
        return end;
    }

    public void SetBoolEnd(bool _end)
    {
        end = _end;
    }
}
