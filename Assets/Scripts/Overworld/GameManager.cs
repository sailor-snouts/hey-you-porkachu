using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static int m_referenceCount = 0;
    private static GameManager m_instance;

    Vector2 playerPos;

    public static GameManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        m_referenceCount++;
        if (m_referenceCount > 1)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        m_instance = this;
        DontDestroyOnLoad(this.gameObject);
        playerPos = this.transform.position;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        PlayerMovementController player = FindObjectOfType<PlayerMovementController>();
        if (player)
        {
            playerPos = player.PlayerPosition();
        }
     }

    // @TODO Params for which battle!
    public void LoadBattle() 
    {
        SceneManager.LoadScene("Battle");
    }

    public void LoadRestaurant()
    {
        SceneManager.LoadScene("Restaurant");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        PlayerMovementController player = FindObjectOfType<PlayerMovementController>();
        if (player)
        {
            player.MovePlayer(playerPos);
        }
    }

    void OnDestroy()
    {
        m_referenceCount--;
        if (m_referenceCount == 0)
        {
            m_instance = null;
        }
    }
}
