using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueQuestionTrigger : MonoBehaviour
{
    public DialogueQuestion dialogue;

    public void TriggerDialogue(PlayerMovementController movement)
    {
        FindObjectOfType<DialogueQuestionManager>().StartDialogue(this.dialogue, movement);
    }
}
