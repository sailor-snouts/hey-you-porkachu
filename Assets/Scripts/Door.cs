using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public int doorKey = KeyType.UNLOCKED;

    private SpriteRenderer open;
    private SpriteRenderer closed;

    private bool isOpen;

	void Start () {
        foreach ( SpriteRenderer doorSprite in GetComponentsInChildren<SpriteRenderer>() ) {
            if(doorSprite.name == "open") {
                open = doorSprite;
            } else if(doorSprite.name == "closed") {
                closed = doorSprite;
            }
        }

        isOpen = false;
	}

    void Update () {
        open.enabled = isOpen;
        closed.enabled = !isOpen;
	}

    public void TakeAction(PlayerMovementController movementController, Inventory inventory) {
        // Check for key
        if( doorKey > KeyType.UNLOCKED) {
            Debug.Log("Door is locked! Key type: " + doorKey);
            if( inventory.HasKey(doorKey) ) {
                // Unlock the door, change the door state
                // TODO:  Play an unlock sfx
                doorKey = KeyType.UNLOCKED;
                SetOpenState(!isOpen);
            } else {
                Debug.Log("No key!  Attempting to display message");
                DialogueTrigger dialogue = gameObject.GetComponent<DialogueTrigger>();
                dialogue.TriggerDialogue(movementController);
            }
        } else {
            // No lock, change the door state
            SetOpenState(!isOpen);
        }
    }

    private void SetOpenState(bool state) {
        isOpen = state;
        var boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = !state;
    }
}
