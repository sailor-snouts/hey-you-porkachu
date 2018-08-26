using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueQuestionTrigger : MonoBehaviour
{
    public DialogueQuestion dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueQuestionManager>().StartDialogue(this.dialogue);
    }

    public int EndDialogue()
    {
        return FindObjectOfType<DialogueQuestionManager>().EndDialogue();
    }
}
