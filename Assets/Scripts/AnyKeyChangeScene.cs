using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeyChangeScene : MonoBehaviour
{
    [SerializeField]
    public string nextScene;
    [SerializeField]
    private AudioClip select;
    private AudioSource audio;

    public void Awake()
    {
        this.audio = gameObject.GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (Input.anyKey)
        {
            if (this.select != null)
            {
                this.audio.PlayOneShot(this.select);
            }
            SceneManager.LoadScene(nextScene);
        }
    }
}
