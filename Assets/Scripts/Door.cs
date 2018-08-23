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
        isLocked = false;
	}

    void Update () {
        open.enabled = isOpen;
        closed.enabled = !isOpen;
	}

    public bool IsLocked() {
        return isLocked;
    }

    public bool IsOpen() {
        return isOpen;
    }

    public void Open() {
        SetOpenState(true);
    }

    public void Close() {
        SetOpenState(false);
    }

    private void SetOpenState(bool state) {
        isOpen = state;
        var boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = !state;
    }
}
