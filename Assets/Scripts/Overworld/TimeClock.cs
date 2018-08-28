using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeClock : MonoBehaviour
{
    TextMeshProUGUI timerText;
    Timer timer;

    void Awake()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        timer = FindObjectOfType<Timer>();

        timerText = gameObject.GetComponent<TextMeshProUGUI>();
        timerText.text = "5:00";
    }

    private void Update()
    {
        var timeLeft = timer.timeLeft;

        if (!HasReachedEndScene() && timeLeft < 0) {
            SceneManager.LoadScene("Lose");
            return;
        }

        if(HasReachedEndScene()) {
            return;
        }
        
        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = niceTime;
    }

    private bool HasReachedEndScene() {
        Scene scene = SceneManager.GetActiveScene();
        return scene.name == "Victory" || scene.name == "Lose";
    }
}
