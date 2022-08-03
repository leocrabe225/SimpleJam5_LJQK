using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Score_Manager : MonoBehaviour
{
    int durationScore;
    int highScore;

    bool stopwatchActive = false;
    float currentTime;

    public TextMeshProUGUI currentTimeText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        StartStopWatch();
    }

    // Update is called once per frame
    void Update()
    {
        if(stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
 
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            currentTimeText.text = time.ToString(@"mm\:ss\:fff");
        }
    }

    public void StartStopWatch()
    {
        stopwatchActive = true;
    }

    public void StopStopWatch()
    {
        stopwatchActive = false;
    }


    //Save Score
    void OnEnable() 
    {
        highScore = PlayerPrefs.GetInt("score");
    }

    void OnDisable()
    {
        if(durationScore > highScore)
        {
            PlayerPrefs.SetInt("score", durationScore);
        }
    }

    //Save Score End

}
