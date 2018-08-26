using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    [SerializeField]
    public float timeLeft = 300.00f;

    private void Update() {
        timeLeft -= Time.deltaTime;
    }
}
