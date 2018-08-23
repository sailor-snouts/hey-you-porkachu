using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private SpriteRenderer open;
    private SpriteRenderer closed;

    private bool isOpen;
    private bool isLocked;

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

    void Open() {
        isOpen = true;
    }

    void Close() {
        isOpen = false;
    }

}
