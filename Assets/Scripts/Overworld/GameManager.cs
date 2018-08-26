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
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        // Place the player at the right transform

        PlayerMovementController player = FindObjectOfType<PlayerMovementController>();
        player.MovePlayer(playerPos);
    }

    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("Scene Unloaded: " + scene.name);

        PlayerMovementController player = FindObjectOfType<PlayerMovementController>();
        playerPos = player.PlayerPosition();
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
