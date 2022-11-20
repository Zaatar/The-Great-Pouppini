using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    UnityEvent whenTimerEnds = null;

    public float timeRemaining = 500;
    public bool timerIsRunning = false;
    public TMP_Text m_TextComponent;
    public TMP_Text m_ScoreComponent;
    public TMP_Text m_finalScoreComponent;
    [SerializeField] private PlayerState player;

    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                m_finalScoreComponent.text = player.getPoints().ToString();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                whenTimerEnds.Invoke();
                   timeRemaining = 0;
                timerIsRunning = false;
            }
            DisplayTime(timeRemaining);
            if (player == null)
            {
                return;
            }
            DisplayScore(player.getPoints());
        }
    }

    void DisplayTime(float timeToDisplay)
    {
 //       float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        m_TextComponent.text = string.Format("{0:00}:{1:000}", seconds, milliSeconds);
    }

    void DisplayScore(int scoreToDisplay)
    {
        m_ScoreComponent.text = scoreToDisplay.ToString();
    }
}
