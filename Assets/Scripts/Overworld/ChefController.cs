using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefController : MonoBehaviour {

    [SerializeField]
    public int chefType;

    public void Start()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if(manager.ChefDefeated(chefType)) {
            Destroy(this.gameObject);
        }
    }

    public void StartEncounter() {
        GameManager manager = FindObjectOfType<GameManager>();
        manager.LoadBattle(this);
    }

    public void EndEncounter() {
        // Check for any items the player won, do that if so
        // Otherwise destroy and bail
        GameManager manager = FindObjectOfType<GameManager>();
        manager.EndBattle(true);
    }
}
