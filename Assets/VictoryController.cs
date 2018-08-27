using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour {

    public float dialogueDelay = 3.5f;

	void Start () {
        Invoke("PresentDialogue", dialogueDelay);
	}

    void PresentDialogue() {
        DialogueTrigger trigger = gameObject.GetComponent<DialogueTrigger>();
        trigger.TriggerDialogue(null);
    }

}
