using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
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

        if (Input.GetButtonDown("Fire1"))
        {
            this.DisplayNextSentence();
        }
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

    public void StartDialogue(Dialogue dialogue, PlayerMovementController movement)
    {
        this.movement = movement;
        this.movement.SetLocked(true);            
        this.setRenderers(true);
        this.nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        nameText.text = "";
        this.setRenderers(false);
        this.movement.SetLocked(false);
    }
}