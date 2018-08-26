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
            Debug.Log("I've been defeated! " + chefType);
            Destroy(this.gameObject);
        } else {
            Debug.Log("No proof that I was defeated! " + chefType);
        }

        Initialize();
    }

    public virtual void Initialize() {

    }

    public virtual void StartEncounter() {
        GameManager manager = FindObjectOfType<GameManager>();
        manager.currentChef = chefType;
        DialogueTrigger battle = gameObject.GetComponent<DialogueTrigger>();
        battle.TriggerDialogue(FindObjectOfType<PlayerMovementController>());
    }

    public void EndEncounter() {
        // Check for any items the player won, do that if so
        // Otherwise destroy and bail
        GameManager manager = FindObjectOfType<GameManager>();
        manager.EndBattle(true);
    }
}
