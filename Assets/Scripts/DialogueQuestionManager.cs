using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueQuestionManager : MonoBehaviour
{
    public TextMeshProUGUI question;
    public TextMeshProUGUI answer1;
    public TextMeshProUGUI answer2;
    public TextMeshProUGUI answer3;
    public TextMeshProUGUI answer4;
    private Renderer rend;
    private PlayerMovementController movement;

    private Queue<string> sentences;

    void Awake()
    {
        sentences = new Queue<string>();
        rend = GetComponent<Renderer>();
        this.setRenderers(false);
    }

    void Update()
    {
        if (!this.isHavingDialogue())
        {
            return;
        }

        // @TODO handle input
    }

    public bool isHavingDialogue()
    {
        return rend.enabled;
    }

    public bool hasMoreDialogue()
    {
        return sentences.Count > 0;
    }

    private void setRenderers(bool enabled)
    {
        rend.enabled = enabled;
    }

    public void StartDialogue(DialogueQuestion dialogue, PlayerMovementController movement)
    {
        this.movement = movement;
        this.movement.SetLocked(true);
        this.setRenderers(true);
        
        sentences.Clear();

        this.question.text = dialogue.question;
        this.answer1.text = dialogue.answers[0];
        this.answer2.text = dialogue.answers[1];
        this.answer3.text = dialogue.answers[2];
        this.answer4.text = dialogue.answers[3];
    }

    void EndDialogue()
    {
        this.question.text = "";
        this.answer1.text = "";
        this.answer2.text = "";
        this.answer3.text = "";
        this.answer4.text = "";
        this.setRenderers(false);
        this.movement.SetLocked(false);
    }
}