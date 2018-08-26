using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static int m_referenceCount = 0;
    private static GameManager m_instance;

    Vector2 playerPos;
    public int currentChef = -1;
    List<int> defeatedChefs;


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
        defeatedChefs = new List<int>();
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

    public void LoadBattle(ChefController chef) 
    {
        // @TODO: Set the type based on the chef that was passed in?
        currentChef = chef.chefType;
        SceneManager.LoadScene("Battle");
    }

    public void EndBattle(bool win) {

        if (win) {
            defeatedChefs.Add(currentChef);
            SceneManager.LoadScene("Restaurant");
            //battleChef.EndEncounter();
        }
    }

    public bool ChefDefeated(int type) {
        foreach(int chef in defeatedChefs) {
            if (chef == type)
                return true;
        }
        return false;
    }

    public void LoadRestaurant()
    {
        SceneManager.LoadScene("Restaurant");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
