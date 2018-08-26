using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    [SerializeField]
    float timeLeft = 300.00f;
    TextMeshProUGUI timerText;

    // Use this for initialization
    void Awake() {
        timerText = gameObject.GetComponent<TextMeshProUGUI>();
        timerText.text = "5:00";
    }

    private void Update() {
        timeLeft -= Time.deltaTime;
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
