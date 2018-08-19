using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashVideo : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    private int sceneSwitchDelay = 3;
    [SerializeField]
    private string nextScene;
    private UnityEngine.Video.VideoPlayer video;
    private AudioSource audioTrack;

    public void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnEnable()
    {
        video = GetComponent<VideoPlayer>();
        audioTrack = GetComponent<AudioSource>();
        video.loopPointReached += Video_loopPointReached;
    }

    private void OnDisable()
    {
        video.loopPointReached -= Video_loopPointReached;
    }

    private void Video_loopPointReached(VideoPlayer source)
    {
        Invoke("SwitchScene", sceneSwitchDelay);
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
