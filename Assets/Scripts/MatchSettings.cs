using System.Collections;
using UnityEngine;

[System.Serializable]
public class MatchSettings : MonoBehaviour {

    [HideInInspector]
    public bool matchInProgress = false;

    [HideInInspector]
    public bool matchFinish = false;

    [HideInInspector]
    public int timeMatch = 600;

    [HideInInspector]
    public int timeRemaining;

    private void Awake()
    {
        timeRemaining = timeMatch;
    }

    private void Update()
    {
        if (matchInProgress)
        {
            StartCoroutine(ReduceTime());
        }
    }

    private IEnumerator ReduceTime()
    {     
        if (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1;
        }
        else
        {
            matchInProgress = false;
            matchFinish = true;
        }
    }
}
