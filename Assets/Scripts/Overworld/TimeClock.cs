using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeClock : MonoBehaviour
{
    TextMeshProUGUI timerText;
    Timer timer;

    // Use this for initialization
    void Awake()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        timer = manager.GetComponent<Timer>();

        timerText = gameObject.GetComponent<TextMeshProUGUI>();
        timerText.text = "5:00";
    }

    private void Update()
    {
        var timeLeft = timer.timeLeft;
        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = niceTime;

        if (timeLeft < 0)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
