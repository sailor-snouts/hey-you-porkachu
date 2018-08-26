using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public int triggerId;

    public void TriggerDialogue(PlayerMovementController movement)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(this.dialogue, movement);
    }
}