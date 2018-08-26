using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private Renderer rend;
    private PlayerMovementController movement;
    private Queue<string> sentences;
    private int scene;
    [SerializeField]
    private AudioClip select;
    private AudioSource audio;

    void Awake()
    {
        sentences = new Queue<string>();
        rend = GetComponent<Renderer>();
        this.setRenderers(false);
        this.audio = gameObject.GetComponent<AudioSource>();
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
        if (Input.GetButton("Fire2"))
        {
            this.EndDialogue();
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
        this.scene = dialogue.scene;
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
        if (this.select != null)
        {
            this.audio.PlayOneShot(this.select);
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        nameText.text = "";
        this.setRenderers(false);
        this.movement.SetLocked(false);

        if(this.scene > 0)
        {
            Debug.Log("Moving from Dialogue to scene " + this.scene);
            SceneManager.LoadScene(this.scene);
        }
    }
}