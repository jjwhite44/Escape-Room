using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI displayTime;

    public TextMeshProUGUI gameOverText;

    public float timeLeft; //2700 for 45 mins
    public bool timerOn = false;

    void Start()
    {
        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }

            else
            {
                timeLeft = 0;
                timerOn = false;
                gameOverText.gameObject.SetActive(true); //displays game over text
                displayTime.gameObject.SetActive(false); //removes timer text
                Time.timeScale = 0; //freezes player (they can still look around)
            }
        }

    }
    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        if (seconds < 10)
        {
            displayTime.text = minutes + ":0" + seconds;
        }
        else
        {
            displayTime.text = minutes + ":" + seconds;
        }
        
    }
}